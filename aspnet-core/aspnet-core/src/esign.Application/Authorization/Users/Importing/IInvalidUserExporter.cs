using System.Collections.Generic;
using esign.Authorization.Users.Importing.Dto;
using esign.Dto;

namespace esign.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
