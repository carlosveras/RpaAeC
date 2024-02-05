using RpaAeC.Data.Repositories;
using RpaAeC.Domain.Abstractions;
using RpaAeC.Domain.Entities;
using RpaAeC.Domain.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace RpaAeC.Services
{
    public class SearchTrainingService : ISearchSearchTrainingService
    {
        private readonly ISearchResultRepository _searchResultRepository;
        private readonly IWebDriver _driver;

        public SearchTrainingService(ISearchResultRepository searchResultRepository)
        {
            _searchResultRepository = searchResultRepository;
            _driver = new FirefoxDriver();
        }

        public async Task OpenBrowser(string url)
        {
            _driver.Navigate().GoToUrl(url);
            await Task.CompletedTask;
        }

        public async Task<Training<List<SearchTraining>>> SearchTrainingchAsync(string query)
        {
            if (!IsElementPresent(By.Id("header-barraBusca-form-campoBusca")))
                return "Erro ao pesquisar a TAG";

            _driver.FindElement(By.Id("header-barraBusca-form-campoBusca")).SendKeys(query);

            IWebElement botaoPesquisar = _driver.FindElement(By.XPath("/html/body/main/section[1]/header/div/nav/div[2]/form/button"));

            if (botaoPesquisar == null)
                return "Erro ao pesquisar a TAG";

            botaoPesquisar.Click();

            var results = GetResultsFromElements();
            await _searchResultRepository.AddResultsBulkAsync(results);

            return await Task.FromResult(results);
        }

        private List<SearchTraining> GetResultsFromElements()
        {
            var results = new List<SearchTraining>();
            var ul = _driver.FindElement(By.ClassName("paginacao-pagina"));
            var lis = ul.FindElements(By.TagName("li"));
            var liCount = lis.Count;

            for (int counter = 1; counter <= liCount; counter++)
            {
                var li = _driver
                    .FindElement(By.ClassName("paginacao-pagina"))
                    .FindElement(By.XPath($"//*[@id=\"busca-resultados\"]/ul/li[{counter}]"));

                var titulo = li.FindElement(By.ClassName("busca-resultado-nome")).Text;
                var descricao = li.FindElement(By.ClassName("busca-resultado-descricao")).Text;

                var link = li.FindElement(By.TagName("a"));

                link.Click();

                if (_driver.Title.Contains("Login"))
                {
                    _driver.Navigate().Back();
                    continue;
                }

                var cargaHoraria = GetCargaHoraria();
                var nomesProfessores = GetNomesProfessores();

                var searchResult = new SearchTraining
                {
                    Id = Guid.NewGuid(),
                    Titulo = titulo,
                    Descricao = descricao,
                    Professor = nomesProfessores,
                    CargaHoraria = cargaHoraria
                };

                results.Add(searchResult);
                _driver.Navigate().Back();
            }

            return results;
        }

        private string GetCargaHoraria()
        {
            if (IsElementPresent(By.ClassName("formacao__info-destaque")))
                return _driver.FindElement(By.ClassName("formacao__info-destaque")).Text;

            if (IsElementPresent(By.ClassName("courseInfo-card-wrapper-infos")))
                return _driver.FindElement(By.ClassName("courseInfo-card-wrapper-infos")).Text;

            return "";
        }

        private string GetNomesProfessores()
        {
            List<string> nomesProfessores = [];
            if (!IsElementPresent(By.ClassName("formacao-instrutores-lista")))
                return GetNomeProfessoresBySectionXPath();

            var professoresUl = _driver.FindElement(By.ClassName("formacao-instrutores-lista"));
            var professoresLi = professoresUl.FindElements(By.TagName("li"));

            foreach (var professor in professoresLi)
            {
                if (!IsElementPresent(By.ClassName("formacao-instrutor-nome")))
                    continue;

                var nome = professor.FindElement(By.ClassName("formacao-instrutor-nome")).Text;
                if (!string.IsNullOrEmpty(nome))
                    nomesProfessores.Add(nome);
            }

            return string.Join(", ", nomesProfessores);
        }

        private string GetNomeProfessoresBySectionXPath()
        {
            var sectionXPath = "/html/body/section[2]/div[1]/section";

            if (!IsElementPresent(By.XPath(sectionXPath))) return "";

            var section = _driver.FindElement(By.XPath(sectionXPath));

            var divs = section.FindElements(By.XPath("/html/body/section[2]/div[1]/section/div"));
            var divCount = divs.Count;

            List<string> nomesProfessores = [];

            for (int i = 1; i <= divCount; i++)
            {
                var nome = _driver
                    .FindElement(By.XPath($"/html/body/section[2]/div[1]/section/div[{i}]"))
                    .FindElement(By.ClassName("instructor-title--name"))
                    .Text;

                if (!string.IsNullOrEmpty(nome))
                    nomesProfessores.Add(nome);
            }

            return string.Join(", ", nomesProfessores);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}