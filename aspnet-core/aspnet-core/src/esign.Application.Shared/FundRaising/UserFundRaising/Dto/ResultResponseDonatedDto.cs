using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class ResultResponseDonatedDto
    {
        public BatchHeader batch_header { get; set; }
        public List<Link> links { get; set; }
    }
    public class BatchHeader
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public SenderBatchHeader sender_batch_header { get; set; }

    }
    public class SenderBatchHeader
    {
        public string email_subject { get; set; }
        public string recipient_type { get; set; }
    }
    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }
    }

}
