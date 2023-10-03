using System.Collections.Generic;
using Abp;
using esign.Chat.Dto;
using esign.Dto;

namespace esign.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
