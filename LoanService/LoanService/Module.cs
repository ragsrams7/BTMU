using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LoanService
{
    public class Module : IHttpModule
    {
        public void Init(HttpApplication applicationcontext)
        {
            applicationcontext.PreSendRequestContent += new EventHandler(Applicationcontext_PreSendRequestContent);
            applicationcontext.PreRequestHandlerExecute += new EventHandler(Applicationcontext_PreRequestHandlerExecute);
            applicationcontext.BeginRequest += new EventHandler(Applicationcontext_BeginRequest);
            applicationcontext.EndRequest += new EventHandler(Applicationcontext_EndRequest);
            
             HttpRequest request = ((HttpApplication)sender).Request;
            HttpContext context = ((HttpApplication)sender).Context;
            XDocument doc = XDocument.Load(context.Request.InputStream);
           
            var result = doc.Root.Attributes().
                        Where(a => a.IsNamespaceDeclaration && a.Name.Namespace == "http://us.mufg.jp/Applications/LoansApp/OVSSubmissionStatus").Select(a => a.Name.Namespace);
            if (result!=null)
            {
                string transactionID = (string)doc.Root.Attribute("TransactionId");
            }
            // IncomingWebRequestContext request1 = WebOperationContext.Current.IncomingRequest;

            System.IO.Stream req = context.Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string str = new System.IO.StreamReader(req).ReadToEnd();
            if (str!=null)
            {

                //XDocument doc = XDocument.Parse(str);
                // XNamespace ns = " http://us.mufg.jp/Applications/LoansApp/OVSSubmissionStatus";

                //var result = doc.Root.Attributes().
                //        Where(a => a.IsNamespaceDeclaration && a.Name.Namespace == "http://us.mufg.jp/Applications/LoansApp/OVSSubmissionStatus").Select(a => a.Name.Namespace);
                       

                //var id = doc.Descendants(ns+"OVSStatus").FirstOrDefault().Value;
            }

        }

        private void Applicationcontext_EndRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Applicationcontext_BeginRequest(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Applicationcontext_PreSendRequestContent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Applicationcontext_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpRequest request = ((HttpApplication)sender).Request;
            HttpContext context = ((HttpApplication)sender).Context;
            Stream HttpStream = context.Request.InputStream;
            HttpStream.Position = 0;
            string filePath = context.Request.FilePath;
            string fileExtension =
                VirtualPathUtility.GetExtension(filePath);
            if (fileExtension.Equals(".svc"))
            {

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
