using System.Collections.Generic;
using esign.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace esign.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
