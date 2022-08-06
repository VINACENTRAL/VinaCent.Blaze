using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using VinaCent.Blaze.AppCore.CommonDatas.Dto;

namespace VinaCent.Blaze.AppCore.CommonDatas
{
    public interface ICommonDataAppService : IApplicationService
    {
        Task<ListResultDto<CommonDataDto>> GetAllList(PagedCommonDataResultRequestDto input);

        //Task<List<T>> GetListType<T>(string type) where T : EntityDto<Guid>;
        //Task<List<T>> SaveListType<T>(string type, params T[] inputs) where T : EntityDto<Guid>;
        //Task<List<T>> SaveAndRemoveOldListType<T>(string type, params T[] inputs) where T : EntityDto<Guid>;
        //Task<T> SaveType<T>(string type, T input) where T : EntityDto<Guid>;

        //Task<T> UpdateType<T>(T input) where T : EntityDto<Guid>;
        //Task<T> CreateType<T>(string type, T input) where T : EntityDto<Guid>;
        //Task<T> GetType<T>(string id) where T : EntityDto<Guid>;
        //Task DeleteType<T>(string id) where T : EntityDto<Guid>;
    }
}
