using System.Linq;
using System.Text.RegularExpressions;
using JiraQueries.JiraRestApi.Models;

namespace JiraQueries.Bussiness.Models {
    public sealed class CustomFieldsViewModel {
        private readonly JiraIssueField _fields;

        public CustomFieldsViewModel(JiraIssueField fields) {
            _fields = fields;
        }

        public string Chamado {
            get {
                if (_fields.IssueType != JiraConsts.IssueTypeBug) {
                    return default;
                }

                var regex = new Regex("(INC ([ -]?(((SDW-)|PROD)?\\d*)[\\, e]?){1,}){1,}", RegexOptions.IgnoreCase);
                if (regex.Match(_fields.Summary).Success) {
                    return Resource.ServiceDesk;
                }

                return Resource.Internal;
            }
        }

        public string CausaRaiz {
            get {
                if (_fields.IssueType != JiraConsts.IssueTypeBug) {
                    return default;
                }

                return string.IsNullOrEmpty(_fields.CausaRaiz)
                    ? Resource.Unspecified
                    : _fields.CausaRaiz;
            }
        }

        public string Fonte {
            get {
                if (_fields.IssueType != JiraConsts.IssueTypeBug) {
                    return default;
                }
                return string.IsNullOrEmpty(_fields.Fonte)
                    ? Resource.Unspecified
                    : _fields.Fonte;
            }
        }

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