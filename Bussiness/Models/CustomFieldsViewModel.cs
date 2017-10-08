using System.Linq;
using System.Text.RegularExpressions;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class CustomFieldsViewModel {
        private readonly JiraIssueField _fields;

        public CustomFieldsViewModel(JiraIssueField fields) {
            _fields = fields;
        }

        public BoolViewModel Chamado {
            get {
                if (_fields.IssueType == "Bug") {
                    var regex = new Regex("(INC ([ -]?(((SDW-)|PROD)?\\d*)[\\, e]?){1,}){1,}", RegexOptions.IgnoreCase);
                    return regex.Match(_fields.Summary).Success;
                }
                return default;
            }
        }

        public string CausaRaiz => _fields.IssueType == "Bug" ? _fields.CausaRaiz : default;

        public string Fonte => _fields.IssueType == "Bug" ? _fields.Fonte : default;

        public string FinanciadorImplementacao => _fields.FinanciadorImplementacao;

        public string FatorProdutizacao => _fields.FatorProdutizacao;

        public string FatorAtualizacaoTecnologica => _fields.FatorAtualizacaoTecnologica;

        public string[] Contratos => _fields.Contratos?.Select(contrato => (string) contrato).ToArray();
    }
}