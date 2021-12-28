using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Export
{
    public static class StaticString
    {
        public static string StructureOneMoreDataTypeFormat = "类型{0}既是枚举又是类";
        public static string WrongEnumIndexFormat = "非法枚举序号，类型名：{0}，序号：{1}";
        public static string SameEnumIndexFormat = "相同枚举序号，类型名：{0}，序号：{1}";
        public static string SameEnumNameFormat = "相同枚举名，类型名：{0}，枚举名：{1}";
        public static string SameClassMemberNameFormat = "相同类成员名，类型名：{0}，成员名：{1}";
        public static string WrongClassMemberTypeNotExistFormat = "非法类成员类型，类型名：{0}，成员名：{1}，成员类型：{2}，此成员类型不存在";
        public static string WrongClassMemberTypeWrongTypeFormat = "非法类成员类型，类型名：{0}，成员名：{1}，成员类型：{2}，此成员类型不能为{3}类型";
        public static string WrongClassMemberDataWrongEnumMemberFormat = "非法类成员枚举数据，类型名：{0}，Id：{1}，成员名：{2}，成员数据{3}不在{4}中";
        public static string WrongDictionaryFormat = "非法Dictionary结构，类型名：{0}";
        public static string SameDataIdFormat = "存在相同的Id，类型名：{0}，Id：{1}";
        public static string WrongMemberPairFormat = "非法成员定义：{0}";
        public static string WrongFieldValueFormat = "非法成员变量：类名：{0}，变量名：{1},Id：{2}";

        public static string AddWrongTypeMemberFormat = "不能加错误的成员，类型名：{0}，成员名：{1}";



        public static string EmptyRowInStructureSheetFormat = "Structure表中的结构不正确，文件名：{0}，表名：{1}，行号:{2}";
        public static string WrongStructureStructureNameFormat = "非法结构类型名，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";
        public static string WrongStructureMemberStructureNameFormat = "非法结构成员类型名，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";
        public static string WrongStructureMemberNameFormat = "非法结构成员名，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";

        public static string WrongEnumMemberNameFormat = "非法枚举成员名，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";

        //public static string WrongEnumIndexFormat = "非法枚举序号，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";
        public static string WrongIdFormat = "非法Id号，文件名：{0}，表名：{1}，行号:{2}，列号：{3}";
        public static string WrongListFormat = "非法List结构，类型名：{0}，成员名：{1}";
        public static string LackTagRowFormat = "缺少标签行，文件名：{0}，表名：{1}，行号:{2}";
        public static string LackFieldNameRowFormat = "缺少标签行，文件名：{0}，表名：{1}，行号:{2}";
        public static string LackFieldStructureNameRowFormat = "缺少标签行，文件名：{0}，表名：{1}，行号:{2}";
        public static string SameFieldNameFormat = "定义同名成员，类型名：{0}，成员名：{1}";
        public static string CannotDefineMultiCollectionFormat = "类型不能同时拥有多个组合，类型名：{0}，成员名：{1}, 成员类型名：{2}";
    }
}