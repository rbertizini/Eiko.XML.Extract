using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Eiko.XML.Extract
{
    class Program
    {
        static void Main(string[] args)
        {
            procxml();
            Console.ReadKey();
        }

        private static void procxml()
        {
            string strXml = "";
            strXml += "<?xml version='1.0' encoding='utf - 8'?><soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><soap:Body><SendResponse xmlns='http://www.nddigital.com.br/webtransfer'><SendResult>&lt;?xml version='1.0' encoding='utf-8'?&gt;";
            strXml += "&lt;CrossTalk_Message xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns='http://www.nddigital.com.br/webtransfer'&gt;";
            strXml += "&lt;CrossTalk_Header&gt;";
            strXml += "&lt;ResponseCode&gt;0&lt;/ResponseCode&gt;";
            strXml += "&lt;ProcessCode&gt;1000&lt;/ProcessCode&gt;";
            strXml += "&lt;MessageType&gt;104&lt;/MessageType&gt;";
            strXml += "&lt;ExchangePattern&gt;1&lt;/ExchangePattern&gt;";
            strXml += "&lt;SourceId&gt;ge_nfse&lt;/SourceId&gt;";
            strXml += "&lt;DateTime&gt;2022-09-23T17:22:20.3656175-03:00&lt;/DateTime&gt;";
            strXml += "&lt;ContentEncoding&gt;utf-8&lt;/ContentEncoding&gt;";
            strXml += "&lt;/CrossTalk_Header&gt;";
            strXml += "&lt;CrossTalk_Body&gt;";
            strXml += "&lt;MessageResponseList&gt;";
            strXml += "&lt;MessageResponse&gt;";
            strXml += "&lt;Code&gt;100&lt;/Code&gt;";
            strXml += "&lt;Description&gt;Operação realizada com sucesso.&lt;/Description&gt;";
            strXml += "&lt;/MessageResponse&gt;";
            strXml += "&lt;/MessageResponseList&gt;";
            strXml += "&lt;SearchDataReturn&gt;";
            strXml += "&lt;DataTransfer&gt;";
            strXml += "&lt;DataName&gt;ge_nfse#14092022150742461-00011462-00510077000101.xml&lt;/DataName&gt;";
            strXml += "&lt;/DataTransfer&gt;";
            strXml += "&lt;DataTransfer&gt;";
            strXml += "&lt;DataName&gt;ge_nfse#16092022194352540-1705-25223194000115.xml&lt;/DataName&gt;";
            strXml += "&lt;/DataTransfer&gt;";
            strXml += "&lt;DataTransfer&gt;";
            strXml += "&lt;DataName&gt;ge_nfse#16092022230249766-627443-61797924000236.xml&lt;/DataName&gt;";
            strXml += "&lt;/DataTransfer&gt;";
            strXml += "&lt;/SearchDataReturn&gt;";
            strXml += "&lt;/CrossTalk_Body&gt;";
            strXml += "&lt;/CrossTalk_Message&gt;</SendResult></SendResponse></soap:Body></soap:Envelope>";

            //Carga do XML
            XmlDocument docXml = new XmlDocument();
            docXml.LoadXml(strXml);

            //Remoção de conteúdo interno
            XmlNodeList itemList = docXml.GetElementsByTagName("SendResult");
            foreach(XmlNode item in itemList)
            {
                string strXmlIns = item.InnerXml;

                if (!string.IsNullOrEmpty(strXmlIns))
                {
                    //Parse do conteúdo interno
                    strXmlIns = HttpUtility.HtmlDecode(strXmlIns);

                    //Criando lista de itens na tag DataName
                    var arqList = new List<string>();
                    try
                    {
                        //Carga do XML
                        XDocument xdocument = XDocument.Parse(strXmlIns);
                        var docNamespace = xdocument.Root.Name.Namespace;
                        
                        //Determinação da tag
                        var tags = xdocument.Descendants(docNamespace + "DataName");

                        //Loop pelas tags
                        foreach (var tag in tags)
                        {
                            var value = tag?.Value;
                            if (!string.IsNullOrEmpty(value) && !arqList.Contains(value))
                            {
                                //Adicionando conteúdo na lista
                                arqList.Add(value);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    //Verificando saída
                    foreach(string arq in arqList)
                    {
                        Console.WriteLine(arq);
                    }
                }
            }
        }
    }
}
