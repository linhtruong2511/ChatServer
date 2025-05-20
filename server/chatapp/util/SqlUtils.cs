using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace chatapp.util
{
    internal class SqlUtils<T> where T : new()
    {
        private SqlUtils() { }
        /// <summary>
        /// đọc trường của thực thể và ghi nó vào thuộc tính tương ứng của lớp đại diện
        /// điều kiện bắt buộc là tên của các thuộc tính phải trùng với tên trong database(không phân biệt chữ hoa chữ thường)
        /// </summary>
        /// <param Name="reader">SqlDataReader</param>
        /// <returns>lớp tương ứng của thực thể</returns>
        public static T SqlReaderToEntity(SqlDataReader reader)
        {
            T result = new T();// tạo 1 thể hiện của lớp T 
            PropertyInfo[] propertyInfo = result.GetType().GetProperties();// lấy ra mảng chứa thông tin các thuộc tính
            for (int i = 0; i < reader.FieldCount; i++)
            {
                for (int j = 0; j < propertyInfo.Length; j++)
                {
                    if (reader.GetName(i).Equals(propertyInfo[j].Name, StringComparison.InvariantCultureIgnoreCase)) //tìm tên cột trùng với thuộc tính
                    {
                        object value = reader.GetValue(i); // lấy giá trị của cột đó
                        Type propType = propertyInfo[j].PropertyType; // lấy kiểu của cột đó trong database
                        value = Convert.ChangeType(value, propType); //chuyển kiểu của cột sang kiểu tương ứng C#
                        propertyInfo[j].SetValue(result, value); // đặt giá trị của cột (vừa chuyển kiểu) vào thuộc tính tương ứng của T
                    }
                }
            }
            return result;
        }
    }

}
