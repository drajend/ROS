﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ROSV2P.WISEService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ag.aig.com", ConfigurationName="WISEService.VBWSAgent")]
    public interface VBWSAgent {
        
        // CODEGEN: Generating message contract since message part namespace () does not match the default value (http://ag.aig.com)
        [System.ServiceModel.OperationContractAttribute(Action="sendIRRequest", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendIRRequestReturn")]
        ROSV2P.WISEService.sendIRRequestResponse sendIRRequest(ROSV2P.WISEService.sendIRRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="sendIRRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<ROSV2P.WISEService.sendIRRequestResponse> sendIRRequestAsync(ROSV2P.WISEService.sendIRRequestRequest request);
        
        // CODEGEN: Generating message contract since message part namespace () does not match the default value (http://ag.aig.com)
        [System.ServiceModel.OperationContractAttribute(Action="sendMVRRequest", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendMVRRequestReturn")]
        ROSV2P.WISEService.sendMVRRequestResponse sendMVRRequest(ROSV2P.WISEService.sendMVRRequestRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="sendMVRRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<ROSV2P.WISEService.sendMVRRequestResponse> sendMVRRequestAsync(ROSV2P.WISEService.sendMVRRequestRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendIRRequest", WrapperNamespace="http://ag.aig.com", IsWrapped=true)]
    public partial class sendIRRequestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string inputIR;
        
        public sendIRRequestRequest() {
        }
        
        public sendIRRequestRequest(string inputIR) {
            this.inputIR = inputIR;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendIRRequestResponse", WrapperNamespace="http://ag.aig.com", IsWrapped=true)]
    public partial class sendIRRequestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string sendIRRequestReturn;
        
        public sendIRRequestResponse() {
        }
        
        public sendIRRequestResponse(string sendIRRequestReturn) {
            this.sendIRRequestReturn = sendIRRequestReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendMVRRequest", WrapperNamespace="http://ag.aig.com", IsWrapped=true)]
    public partial class sendMVRRequestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string inputMVR;
        
        public sendMVRRequestRequest() {
        }
        
        public sendMVRRequestRequest(string inputMVR) {
            this.inputMVR = inputMVR;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendMVRRequestResponse", WrapperNamespace="http://ag.aig.com", IsWrapped=true)]
    public partial class sendMVRRequestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="", Order=0)]
        public string sendMVRRequestReturn;
        
        public sendMVRRequestResponse() {
        }
        
        public sendMVRRequestResponse(string sendMVRRequestReturn) {
            this.sendMVRRequestReturn = sendMVRRequestReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface VBWSAgentChannel : ROSV2P.WISEService.VBWSAgent, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VBWSAgentClient : System.ServiceModel.ClientBase<ROSV2P.WISEService.VBWSAgent>, ROSV2P.WISEService.VBWSAgent {
        
        public VBWSAgentClient() {
        }
        
        public VBWSAgentClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VBWSAgentClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VBWSAgentClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VBWSAgentClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ROSV2P.WISEService.sendIRRequestResponse ROSV2P.WISEService.VBWSAgent.sendIRRequest(ROSV2P.WISEService.sendIRRequestRequest request) {
            return base.Channel.sendIRRequest(request);
        }
        
        public string sendIRRequest(string inputIR) {
            ROSV2P.WISEService.sendIRRequestRequest inValue = new ROSV2P.WISEService.sendIRRequestRequest();
            inValue.inputIR = inputIR;
            ROSV2P.WISEService.sendIRRequestResponse retVal = ((ROSV2P.WISEService.VBWSAgent)(this)).sendIRRequest(inValue);
            return retVal.sendIRRequestReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ROSV2P.WISEService.sendIRRequestResponse> ROSV2P.WISEService.VBWSAgent.sendIRRequestAsync(ROSV2P.WISEService.sendIRRequestRequest request) {
            return base.Channel.sendIRRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<ROSV2P.WISEService.sendIRRequestResponse> sendIRRequestAsync(string inputIR) {
            ROSV2P.WISEService.sendIRRequestRequest inValue = new ROSV2P.WISEService.sendIRRequestRequest();
            inValue.inputIR = inputIR;
            return ((ROSV2P.WISEService.VBWSAgent)(this)).sendIRRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ROSV2P.WISEService.sendMVRRequestResponse ROSV2P.WISEService.VBWSAgent.sendMVRRequest(ROSV2P.WISEService.sendMVRRequestRequest request) {
            return base.Channel.sendMVRRequest(request);
        }
        
        public string sendMVRRequest(string inputMVR) {
            ROSV2P.WISEService.sendMVRRequestRequest inValue = new ROSV2P.WISEService.sendMVRRequestRequest();
            inValue.inputMVR = inputMVR;
            ROSV2P.WISEService.sendMVRRequestResponse retVal = ((ROSV2P.WISEService.VBWSAgent)(this)).sendMVRRequest(inValue);
            return retVal.sendMVRRequestReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ROSV2P.WISEService.sendMVRRequestResponse> ROSV2P.WISEService.VBWSAgent.sendMVRRequestAsync(ROSV2P.WISEService.sendMVRRequestRequest request) {
            return base.Channel.sendMVRRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<ROSV2P.WISEService.sendMVRRequestResponse> sendMVRRequestAsync(string inputMVR) {
            ROSV2P.WISEService.sendMVRRequestRequest inValue = new ROSV2P.WISEService.sendMVRRequestRequest();
            inValue.inputMVR = inputMVR;
            return ((ROSV2P.WISEService.VBWSAgent)(this)).sendMVRRequestAsync(inValue);
        }
    }
}