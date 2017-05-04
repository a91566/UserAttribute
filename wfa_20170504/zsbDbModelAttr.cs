using System;
using System.Collections.Generic;
using System.Reflection;

namespace wfa_20170504
{
	/// <summary>
	/// 自定义特性 
	/// 属性、结构体、类可用  
	/// 支持继承
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | System.AttributeTargets.Struct | AttributeTargets.Class, Inherited = true)]
	public class zsbDBAttribute : Attribute
	{
		/// <summary>
		/// 实体实际对应的表名
		/// </summary>
		public string TableName { get; set; }		
		/// <summary>
		/// 列表时是否查询
		/// </summary>
		public bool IsSelect { get; set; }
		/// <summary>
		/// 查询显示的列名
		/// </summary>
		public string HeaderText { get; set; }

	}

	/// <summary>
	/// 实体的基类
	/// </summary>
	public class zsbDBModelBase
	{
		/// <summary>
		/// 获取自定义属性
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static (string TableName, string Fields) GetTableFields<T>() where T : class, new()
		{
			Type type = typeof(T);
			//默认等于类名
			string tableName = type.Name;
			foreach (object obj in type.GetCustomAttributes(typeof(zsbDBAttribute), true))
			{
				if ((obj as zsbDBAttribute) != null)
				{
					tableName = (obj as zsbDBAttribute).TableName;
					break;
				}
			}	
			List<string> list = new List<string>();
			foreach (PropertyInfo propInfo in type.GetProperties())
			{
				object[] attrs = propInfo.GetCustomAttributes(typeof(zsbDBAttribute), true);
				if (attrs.Length > 0 && (attrs[0] as zsbDBAttribute) != null && (attrs[0] as zsbDBAttribute).IsSelect)
				{
					list.Add($"{ propInfo.Name } { (attrs[0] as zsbDBAttribute).HeaderText}");
				}
			}
			return (tableName, string.Join(",", list));
		}
		
	}
	

	[zsbDB(TableName = "T_Person")]
	public class Person : zsbDBModelBase
	{
		/// <summary>
		/// ID
		/// </summary>
		[zsbDB(IsSelect = false)]
		public int ID { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "姓名")]
		public string Name { get; set; }
		/// <summary>
		/// 工作
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "工作")]
		public string Work { get; set; }
		/// <summary>
		/// 职位
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "职位")]
		public string Title { get; set; }
		/// <summary>
		/// 身份证
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "身份证")]
		public string CardID { get; set; }
		/// <summary>
		/// 生日
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "生日")]
		public DateTime Birthday { get; set; }
	}

	[zsbDB(TableName = "T_Student")]
	public class Student : zsbDBModelBase
	{
		/// <summary>
		/// ID
		/// </summary>
		[zsbDB(IsSelect = false)]
		public int ID { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "姓名")]
		public string Name { get; set; }
		/// <summary>
		/// 学校
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "学校")]
		public string School { get; set; }
		/// <summary>
		/// 身份证
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "身份证")]
		public string CardID { get; set; }
		/// <summary>
		/// 生日
		/// </summary>
		[zsbDB(IsSelect = true, HeaderText = "生日")]
		public DateTime Birthday { get; set; }
	}
}
