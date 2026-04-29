Revit 插件 - C# (RevitPlugin.cs)
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BIMPipelineOptimizer
{
    [Transaction(TransactionMode.Manual)]
    public class OptimizePipelineCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, CommandExecutionContext context)
        {
            // 获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;

            // 创建 Agent 管理类实例
            PipelineAgentManager agentManager = new PipelineAgentManager(doc);

            // 启动管线优化
            bool optimizationSuccess = agentManager.OptimizePipeline();

            if (optimizationSuccess)
            {
                TaskDialog.Show("Success", "Pipeline Optimization Completed!");
            }
            else
            {
                TaskDialog.Show("Failure", "Optimization Failed.");
            }

            return Result.Succeeded;
        }
    }

    public class PipelineAgentManager
    {
        private Document doc;
        private static readonly HttpClient client = new HttpClient();

        public PipelineAgentManager(Document doc)
        {
            this.doc = doc;
        }

        public bool OptimizePipeline()
        {
            // 模拟从 Revit 获取空间信息，传递到 Python 后端进行优化
            var requestBody = new
            {
                spaceData = "Revit Space Data Here"  // 这里可以替换为从 Revit 获取的实际数据
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = client.PostAsync("http://127.0.0.1:5000/api/optimize", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                TaskDialog.Show("Success", responseString); // 显示优化结果
                return true;
            }
            return false;
        }
    }
}