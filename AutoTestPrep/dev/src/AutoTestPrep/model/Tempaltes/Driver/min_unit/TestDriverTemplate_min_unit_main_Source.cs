﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 16.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AutoTestPrep.Model.Tempaltes.Driver.min_unit
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class TestDriverTemplate_min_unit_main_Source : TestDriverTemplate_min_unit_Source
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("#include <stdio.h>\r\n#include \"min_unit.h\"\r\n\r\n//Declare function to run test.\r\n");
            
            #line 11 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
 foreach (var testItem in Tests) { 
            
            #line default
            #line hidden
            this.Write("char* ");
            
            #line 12 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateRunTestMethodEntryPointName(testItem.Target)));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 13 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\nint tests_run = 0;\r\n\r\nint main()\r\n{\r\n");
            
            #line 19 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
 foreach (var testItem in Tests) { 
            
            #line default
            #line hidden
            this.Write("\tmu_run_all_test(\"");
            
            #line 20 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateRunTestMethodEntryPointName(testItem.Target)));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 20 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CreateRunTestMethodEntryPointName(testItem.Target)));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 21 "E:\development\TestSupportTools\AutoTestPrep\dev\src\AutoTestPrep\Model\Tempaltes\Driver\min_unit\TestDriverTemplate_min_unit_main_Source.tt"
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