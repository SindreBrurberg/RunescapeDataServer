using System;
using System.IO;
using System.Text;

namespace Handler {
    class Config {
        public Config() {
            var cfgPath = Directory.GetCurrentDirectory() + @"\config.cfg";
            if (File.Exists(cfgPath)) {
            } else {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#RunescapeMinigames data server config");
                sb.AppendLine("Database: {");
                sb.AppendLine("    DataSource: ");
                sb.AppendLine("    Username: ");
                sb.AppendLine("    Password: ");
                sb.AppendLine("    Catalog: ");
                sb.AppendLine("}");
                File.Create(cfgPath);
                File.WriteAllText(cfgPath,sb.ToString());
            }
        }
        public void UpdateValues(string path) {
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
                                if (line.StartsWith("DataSource")) {
                                    Sql.DataSource = line.Substring(line.IndexOf(":")).Replace(" ", "");
                                } else if (line.StartsWith("Username")) {
                                    Sql.Username = line.Substring(line.IndexOf(":")).Replace(" ", "");
                                } else if (line.StartsWith("Password")) {
                                    Sql.Password = line.Substring(line.IndexOf(":")).Replace(" ", "");
                                } else if (line.StartsWith("Catalog")) {
                                    Sql.Catalog = line.Substring(line.IndexOf(":")).Replace(" ", "");
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}