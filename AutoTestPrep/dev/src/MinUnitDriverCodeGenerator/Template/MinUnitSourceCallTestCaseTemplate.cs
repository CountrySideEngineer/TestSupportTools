﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 16.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenerator.TestDriver.Template
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class MinUnitSourceCallTestCaseTemplate : MinUnitTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("char* ");
            
            #line 7 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TargetFunction.Name));
            
            #line default
            #line hidden
            this.Write("_utest_run_all()\r\n{\r\n");
            
            #line 9 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
 foreach (var testCaseItem in this.Test.TestCases) { 
            
            #line default
            #line hidden
            this.Write("\tmu_run_test(\"");
            
            #line 10 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateTestCaseMethodName(this.TargetFunction, testCaseItem)));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 10 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateTestCaseMethodName(this.TargetFunction, testCaseItem)));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 11 "E:\development\TestSupportTools_0_2_0\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceCallTestCaseTemplate.tt"
	} 
            
            #line default
            #line hidden
            this.Write("\r\n\treturn 0;\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}