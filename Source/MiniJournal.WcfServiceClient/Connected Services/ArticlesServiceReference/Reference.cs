﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniJournal.WcfServiceClient.ArticlesServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Article", Namespace="http://schemas.datacontract.org/2004/07/Infotecs.MiniJournal.WcfService.DataTrans" +
        "ferObjects")]
    [System.SerializableAttribute()]
    public partial class Article : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MiniJournal.WcfServiceClient.ArticlesServiceReference.Comment[] CommentsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImageIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MiniJournal.WcfServiceClient.ArticlesServiceReference.User UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MiniJournal.WcfServiceClient.ArticlesServiceReference.Comment[] Comments {
            get {
                return this.CommentsField;
            }
            set {
                if ((object.ReferenceEquals(this.CommentsField, value) != true)) {
                    this.CommentsField = value;
                    this.RaisePropertyChanged("Comments");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImageId {
            get {
                return this.ImageIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageIdField, value) != true)) {
                    this.ImageIdField = value;
                    this.RaisePropertyChanged("ImageId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MiniJournal.WcfServiceClient.ArticlesServiceReference.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Infotecs.MiniJournal.WcfService.DataTrans" +
        "ferObjects")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Comment", Namespace="http://schemas.datacontract.org/2004/07/Infotecs.MiniJournal.WcfService.DataTrans" +
        "ferObjects")]
    [System.SerializableAttribute()]
    public partial class Comment : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MiniJournal.WcfServiceClient.ArticlesServiceReference.User UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MiniJournal.WcfServiceClient.ArticlesServiceReference.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ArticlesServiceReference.IArticlesWebService")]
    public interface IArticlesWebService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/GetArticles", ReplyAction="http://tempuri.org/IArticlesWebService/GetArticlesResponse")]
        MiniJournal.WcfServiceClient.ArticlesServiceReference.Article[] GetArticles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/GetArticles", ReplyAction="http://tempuri.org/IArticlesWebService/GetArticlesResponse")]
        System.Threading.Tasks.Task<MiniJournal.WcfServiceClient.ArticlesServiceReference.Article[]> GetArticlesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/CreateArticle", ReplyAction="http://tempuri.org/IArticlesWebService/CreateArticleResponse")]
        void CreateArticle(string text, byte[] image, long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/CreateArticle", ReplyAction="http://tempuri.org/IArticlesWebService/CreateArticleResponse")]
        System.Threading.Tasks.Task CreateArticleAsync(string text, byte[] image, long userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/DeleteArticle", ReplyAction="http://tempuri.org/IArticlesWebService/DeleteArticleResponse")]
        void DeleteArticle(long articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/DeleteArticle", ReplyAction="http://tempuri.org/IArticlesWebService/DeleteArticleResponse")]
        System.Threading.Tasks.Task DeleteArticleAsync(long articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/AddComment", ReplyAction="http://tempuri.org/IArticlesWebService/AddCommentResponse")]
        void AddComment(string text, long userId, long articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/AddComment", ReplyAction="http://tempuri.org/IArticlesWebService/AddCommentResponse")]
        System.Threading.Tasks.Task AddCommentAsync(string text, long userId, long articleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/DeleteComment", ReplyAction="http://tempuri.org/IArticlesWebService/DeleteCommentResponse")]
        void DeleteComment(long articleId, long commentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IArticlesWebService/DeleteComment", ReplyAction="http://tempuri.org/IArticlesWebService/DeleteCommentResponse")]
        System.Threading.Tasks.Task DeleteCommentAsync(long articleId, long commentId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IArticlesWebServiceChannel : MiniJournal.WcfServiceClient.ArticlesServiceReference.IArticlesWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ArticlesWebServiceClient : System.ServiceModel.ClientBase<MiniJournal.WcfServiceClient.ArticlesServiceReference.IArticlesWebService>, MiniJournal.WcfServiceClient.ArticlesServiceReference.IArticlesWebService {
        
        public ArticlesWebServiceClient() {
        }
        
        public ArticlesWebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ArticlesWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ArticlesWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ArticlesWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MiniJournal.WcfServiceClient.ArticlesServiceReference.Article[] GetArticles() {
            return base.Channel.GetArticles();
        }
        
        public System.Threading.Tasks.Task<MiniJournal.WcfServiceClient.ArticlesServiceReference.Article[]> GetArticlesAsync() {
            return base.Channel.GetArticlesAsync();
        }
        
        public void CreateArticle(string text, byte[] image, long userId) {
            base.Channel.CreateArticle(text, image, userId);
        }
        
        public System.Threading.Tasks.Task CreateArticleAsync(string text, byte[] image, long userId) {
            return base.Channel.CreateArticleAsync(text, image, userId);
        }
        
        public void DeleteArticle(long articleId) {
            base.Channel.DeleteArticle(articleId);
        }
        
        public System.Threading.Tasks.Task DeleteArticleAsync(long articleId) {
            return base.Channel.DeleteArticleAsync(articleId);
        }
        
        public void AddComment(string text, long userId, long articleId) {
            base.Channel.AddComment(text, userId, articleId);
        }
        
        public System.Threading.Tasks.Task AddCommentAsync(string text, long userId, long articleId) {
            return base.Channel.AddCommentAsync(text, userId, articleId);
        }
        
        public void DeleteComment(long articleId, long commentId) {
            base.Channel.DeleteComment(articleId, commentId);
        }
        
        public System.Threading.Tasks.Task DeleteCommentAsync(long articleId, long commentId) {
            return base.Channel.DeleteCommentAsync(articleId, commentId);
        }
    }
}