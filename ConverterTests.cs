using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Virinco.WATS.Interface;

namespace TerradyneSpectrumConverter
{
    [TestClass]
    public class ConverterTests : TDM
    {
        [TestMethod]
        public void SetupClient()
        {
            RegisterClient("https://your-site-name.wats.com", "", "your-client-register-token");
            InitializeAPI(true);
        }

        [TestMethod]
        public void TestICTConverter()
        {
            InitializeAPI(true);
            string fn = @"Examples\SampleICTTest.txt";
            Dictionary<string, string> arguments = new TerradyneSpectrumICTConverter().ConverterParameters;
            TerradyneSpectrumICTConverter converter = new TerradyneSpectrumICTConverter(arguments);
            using (FileStream file = new FileStream(fn, FileMode.Open))
            {
                SetConversionSource(new FileInfo(fn), converter.ConverterParameters, null);
                converter.ImportReport(this, file);
                SubmitPendingReports();
            }
        }


        [TestMethod]
        public void TestXMLConverterFolder()
        {
            InitializeAPI(true);
            Dictionary<string, string> arguments = new TerradyneSpectrumICTConverter().ConverterParameters;
            TerradyneSpectrumICTConverter converter = new TerradyneSpectrumICTConverter(arguments);
            foreach (string fn in Directory.GetFiles(@"Data", "*.txt"))
            {
                using (FileStream file = new FileStream(fn, FileMode.Open))
                {
                    SetConversionSource(new FileInfo(fn), converter.ConverterParameters, null);
                    converter.ImportReport(this, file);
                    SubmitPendingReports();
                }
            }
        }
    }
}
