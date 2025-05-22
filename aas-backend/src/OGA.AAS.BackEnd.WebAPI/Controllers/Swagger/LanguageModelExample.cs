using OGA.AAS.BackEnd.Domain.Models;
using OGA.Core.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
    public class LanguageModelExample : IExamplesProvider<LanguageModel>
    {
        public LanguageModel GetExamples()
        {
            return new LanguageModel
            {
                Id = 1,
                Code = "es",
                Name = "LANGUAGE",
                Description = "Language"
            };
        }
        
    }
    [ExcludeFromCodeCoverage]
    public class LanguageModelListExample : IExamplesProvider<IEnumerable<LanguageModel>>
    {
        public IEnumerable<LanguageModel> GetExamples()
        {
            return new List<LanguageModel> {
                new LanguageModelExample().GetExamples()
            };
        }
    }

    [ExcludeFromCodeCoverage]
    public class LanguagePaginationModelExample : IExamplesProvider<DataPaginationModel<LanguageModel>>
    {
        public DataPaginationModel<LanguageModel> GetExamples()
        {
            return new DataPaginationModel<LanguageModel>()
            {
                PageNumber = 1,
                PageSize = 10,
                TotalSize = 100,
                Data = new LanguageModelListExample().GetExamples()
            };
        }
    }
}
