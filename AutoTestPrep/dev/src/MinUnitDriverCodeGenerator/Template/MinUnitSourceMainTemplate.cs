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
    
    #line 1 "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceMainTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class MinUnitSourceMainTemplate : MinUnitTemplate
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("#include <stdio.h>\r\n#include \"min_unit.h\"\r\n\r\nint test_run = 0;\r\n\r\nchar* ");
            
            #line 12 "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TargetFunction.Name));
            
            #line default
            #line hidden
            this.Write("_utest_run_all();\r\n\r\nint main()\r\n{\r\n\tmu_run_all_test(\"");
            
            #line 16 "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TargetFunction.Name));
            
            #line default
            #line hidden
            this.Write("_utest_run_all\", ");
            
            #line 16 "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\MinUnitDriverCodeGenerator\Template\MinUnitSourceMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TargetFunction.Name));
            
            #line default
            #line hidden
            this.Write("_utest_run_all);\r\n\r\n\treturn 0;\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
