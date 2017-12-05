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

        public string Produtizacao => _fields.FatorProdutizacao;

        public double? ProdutizacaoPercentual {
            get {
                switch (_fields.FatorProdutizacao) {
                    case null:
                        return null;
                    case "100% Produto":
                        return 1.0d;
                    case "70% Produto":
                        return 0.7d;
                    case "30% Produto":
                        return 0.3d;
                    case "0% Produto":
                        return 0;
                    default:
                        return null;
                }
            }
        }

        public string AtualizacaoTecnologica => _fields.FatorAtualizacaoTecnologica;

        public double? AtualizacaoTecnologicaPercentual {
            get {
                switch (_fields.FatorAtualizacaoTecnologica) {
                    case null:
                        return null;
                    case "100% - Atualização Tecnológica":
                        return 1.0d;
                    case "70% - Atualização Tecnológica":
                        return 0.7d;
                    case "30% - Atualização Tecnológica":
                        return 0.3d;
                    case "0% - Atualização Tecnológica":
                        return 0;
                    default:
                        return null;
                }
            }
        }

        public string[] Contratos => _fields.Contratos?.Select(contrato => (string) contrato).ToArray();
    }
}