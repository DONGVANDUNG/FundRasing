﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RPA.EXRATE.TOOL.ServiceEmail {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://toyotavn.com.vn/", ConfigurationName="ServiceEmail.EmailServiceSoap")]
    public interface EmailServiceSoap {
        
        // CODEGEN: Generating message contract since element name from from namespace http://toyotavn.com.vn/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://toyotavn.com.vn/SendEmail", ReplyAction="*")]
        RPA.EXRATE.TOOL.ServiceEmail.SendEmailResponse SendEmail(RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendEmailRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendEmail", Namespace="http://toyotavn.com.vn/", Order=0)]
        public RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequestBody Body;
        
        public SendEmailRequest() {
        }
        
        public SendEmailRequest(RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://toyotavn.com.vn/")]
    public partial class SendEmailRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string from;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string to;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string cc;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string bcc;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string subject;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string body;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string attachments;
        
        public SendEmailRequestBody() {
        }
        
        public SendEmailRequestBody(string from, string to, string cc, string bcc, string subject, string body, string attachments) {
            this.from = from;
            this.to = to;
            this.cc = cc;
            this.bcc = bcc;
            this.subject = subject;
            this.body = body;
            this.attachments = attachments;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendEmailResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendEmailResponse", Namespace="http://toyotavn.com.vn/", Order=0)]
        public RPA.EXRATE.TOOL.ServiceEmail.SendEmailResponseBody Body;
        
        public SendEmailResponse() {
        }
        
        public SendEmailResponse(RPA.EXRATE.TOOL.ServiceEmail.SendEmailResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://toyotavn.com.vn/")]
    public partial class SendEmailResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool SendEmailResult;
        
        public SendEmailResponseBody() {
        }
        
        public SendEmailResponseBody(bool SendEmailResult) {
            this.SendEmailResult = SendEmailResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface EmailServiceSoapChannel : RPA.EXRATE.TOOL.ServiceEmail.EmailServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmailServiceSoapClient : System.ServiceModel.ClientBase<RPA.EXRATE.TOOL.ServiceEmail.EmailServiceSoap>, RPA.EXRATE.TOOL.ServiceEmail.EmailServiceSoap {
        
        public EmailServiceSoapClient() {
        }
        
        public EmailServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmailServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RPA.EXRATE.TOOL.ServiceEmail.SendEmailResponse RPA.EXRATE.TOOL.ServiceEmail.EmailServiceSoap.SendEmail(RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequest request) {
            return base.Channel.SendEmail(request);
        }
        
        public bool SendEmail(string from, string to, string cc, string bcc, string subject, string body, string attachments) {
            RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequest inValue = new RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequest();
            inValue.Body = new RPA.EXRATE.TOOL.ServiceEmail.SendEmailRequestBody();
            inValue.Body.from = from;
            inValue.Body.to = to;
            inValue.Body.cc = cc;
            inValue.Body.bcc = bcc;
            inValue.Body.subject = subject;
            inValue.Body.body = body;
            inValue.Body.attachments = attachments;
            RPA.EXRATE.TOOL.ServiceEmail.SendEmailResponse retVal = ((RPA.EXRATE.TOOL.ServiceEmail.EmailServiceSoap)(this)).SendEmail(inValue);
            return retVal.Body.SendEmailResult;
        }
    }
}
