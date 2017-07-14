using System;
using System.IO;
using System.Text;

namespace Handler {
    class ConfigFile {
        public static void init() {
            var cfgPath = Directory.GetCurrentDirectory() + @"\config.cfg";
            if (File.Exists(cfgPath)) {
                UpdateValues(cfgPath);
            } else {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#RunescapeMinigames data server config");
                sb.AppendLine("Database: {");
                sb.AppendLine("    DataSource: ");
                sb.AppendLine("    Username: ");
                sb.AppendLine("    Password: ");
                sb.AppendLine("    Catalog: ");
                sb.AppendLine("}");
                File.Create(cfgPath).Dispose();
                File.WriteAllText(cfgPath,sb.ToString());
            }
        }
        public static void UpdateValues(string path) {
            string[] lines = File.ReadAllLines(path);
            string obj = "";
            foreach (var line in lines) {
                if (!line.StartsWith("#")){
                    if (line.EndsWith("{")) {
                        obj = line.Remove(line.IndexOf(":"));
                    } else if (line.StartsWith("}")) {
                        obj = "";
                    } else {
                        switch (obj) {
                            case "Database":
                                if (line.Contains("DataSource")) {
                                    Sql.DataSource = line.Substring(line.IndexOf(":") + 1).Replace(" ", "");
                                } else if (line.Contains("Username")) {
                                    Sql.Username = line.Substring(line.IndexOf(":") + 1).Replace(" ", "");
                                } else if (line.Contains("Password")) {
                                    Sql.Password = line.Substring(line.IndexOf(":") + 1).Replace(" ", "");
                                } else if (line.Contains("Catalog")) {
                                    Sql.Catalog = line.Substring(line.IndexOf(":") + 1).Replace(" ", "");
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}