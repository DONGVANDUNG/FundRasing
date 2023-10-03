using System.Collections.Generic;
using esign.Authorization.Users.Dto;
using esign.Dto;

namespace esign.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}