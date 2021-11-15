using LiteDB;
using StubDriverPlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.TestStubDriver
{
    public class PluginManager
    {
        /// <summary>
        /// Database table name.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Database file name.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PluginManager()
		{
            this.FilePath = string.Empty;
            this.TableName = string.Empty;
		}

        /// <summary>
        /// Constructor with argument.
        /// </summary>
        /// <param name="filePath">Path to database file path.</param>
        /// <param name="tableName">TableName to access.</param>
        public PluginManager(string filePath, string tableName)
		{
            this.FilePath = filePath;
            this.TableName = tableName;
		}

        /// <summary>
        /// Check whether a plugin information has been registered in database.
        /// </summary>
        /// <param name="pluginInfo">Plugin information to check.</param>
        /// <returns>Returns true if the plugin has been registered, otherwise returns false.</returns>
        public virtual bool IsRegistered(PluginInfo pluginInfo)
		{
            bool isRegistered = false;
            using (var db = new LiteDatabase(this.FilePath))
			{
                isRegistered = this.IsRegistered(db, pluginInfo);
			}
            return isRegistered;
		}

        /// <summary>
        /// Check whether a plugin information has been registered in database.
        /// </summary>
        /// <param name="database">Data base object.</param>
        /// <param name="pluginInfo">Plugin information to check.</param>
        /// <returns>Returns true if the plugin has been registered, otherwise returns false.</returns>
        protected virtual bool IsRegistered(LiteDatabase database, PluginInfo pluginInfo)
		{
            var col = database.GetCollection<PluginInfo>(this.TableName);
            IEnumerable<PluginInfo> pluginInfos = col.Query().ToEnumerable();
            IEnumerable<PluginInfo> matchPluginInfos = pluginInfos.Where(item => 
                item.Name.Equals(pluginInfo.Name) && item.FileName.Equals(pluginInfo.FileName));
            bool isRegistered = false;
            var temp = matchPluginInfos.FirstOrDefault();
            if (0 < matchPluginInfos.Count())
			{
                isRegistered = true;
			}
            return isRegistered;
		}

        /// <summary>
        /// Regist plugin data to database.
        /// </summary>
        /// <param name="pluginInfo">Plugin information to regist into database.</param>
        public virtual void Regist(PluginInfo pluginInfo)
		{
            bool isRegistered = this.IsRegistered(pluginInfo);
            if (!isRegistered)
			{
                using (var database = new LiteDatabase(this.FilePath))
				{
                    this.Regist(database, pluginInfo);
				}
			}
		}

        /// <summary>
        /// Regist plugin data to database.
        /// </summary>
        /// <param name="database">Database object to regist plugin information.</param>
        /// <param name="pluginInfo">Plugin information to regist into database.</param>
        protected virtual void Regist(LiteDatabase database, PluginInfo pluginInfo)
		{
            var col = database.GetCollection<PluginInfo>(this.TableName);
            col.Insert(pluginInfo);
		}

        /// <summary>
        /// Load plugin object specified by argument index from database.
        /// </summary>
        /// <param name="index">Index of plugin.</param>
        /// <returns>Plugin object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Argument <para>index</para> is out of range of plugin database.</exception>
        /// <exception cref="FileNotFoundException">DLL file registered in database can not be found.</exception>
        public virtual IStubDriverPlugin Load(int index)
		{
            try
			{
                IEnumerable<PluginInfo> pluginInfos = this.GetList();
                PluginInfo pluginInfo = pluginInfos.ElementAt(index);
                IStubDriverPlugin plugin = this.Load(pluginInfo);
                return plugin;
            }
            catch (ArgumentOutOfRangeException)
			{
                throw;
			}
            catch (FileNotFoundException)
			{
                throw;
			}
        }

        /// <summary>
        /// Load plugin object specified by argument PluginInfo object.
        /// </summary>
        /// <param name="pluginInfo">Pluin information.</param>
        /// <returns>Plugin object</returns>
        public virtual IStubDriverPlugin Load(PluginInfo pluginInfo)
		{
            try
			{
                var assembly = Assembly.LoadFrom(pluginInfo.FileName);
                var ifType = assembly.GetTypes()
                    .Where(typeItem => typeItem.IsClass && !typeItem.IsAbstract && (typeItem.GetInterface(nameof(IStubDriverPlugin)) != null))
                    .FirstOrDefault();
                if (null == ifType)
                {
                    throw new NullReferenceException();
                }
                IStubDriverPlugin plugin = (IStubDriverPlugin)Activator.CreateInstance(ifType);
                return plugin;
            }
            catch (FileNotFoundException)
			{
                throw;
			}
            catch (ArgumentNullException)
			{
                throw;
			}

        }

        /// <summary>
        /// Check whether the plugin specified by argument index has been registered in database.
        /// </summary>
        /// <param name="index">Index to check.</param>
        /// <returns>Returns true if the plugin has been registered, otherwise returns false.</returns>
        public virtual bool Check(int index)
		{
            IEnumerable<PluginInfo> pluginInfos = this.GetList();
            int count = pluginInfos.Count();
            if (index < count)
			{
                return true;
			}
			else
			{
                return false;
			}
		}

        /// <summary>
        /// Get collection of plugin information registered in database.
        /// </summary>
        /// <returns>Collection of plugin information.</returns>
        public virtual IEnumerable<PluginInfo> GetList()
		{
            using (var db = new LiteDatabase(this.FilePath))
			{
                var col = db.GetCollection<PluginInfo>(this.TableName);
                var query = col.Query().ToEnumerable().ToList();
                return query;
			}
		}
    }
}
