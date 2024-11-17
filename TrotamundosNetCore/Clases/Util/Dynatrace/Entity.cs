namespace TrotamundosNetCore.Clases.Util.Dynatrace
{

    public class RespuestaEntities
    {
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public string nextPageKey { get; set; }
        public List<Entity> entities { get; set; }

    }

    public class Entity
    {
        public string entityId { get; set; }
        public string displayName { get; set; }
        public string type { get; set; }
        public long firstSeenTms { get; set; }
        public long lastSeenTms { get; set; }

        public Properties properties { get; set; }

        public FromRelationships fromRelationships { get; set; }
        public Icon icon { get; set; }
    }

    public class Icon
    {
        public string primaryIconType { get; set; }
    }

    public class Properties
    {
        public string serviceType { get; set; }
        public string agentTechnologyType { get; set; }

        //BD
        public bool EsBD => serviceType == "DATABASE_SERVICE" && !string.IsNullOrEmpty(databaseName);
        public string databaseName { get; set; }
        public List<string> ipAddress { get; set; }
        public List<string> databaseHostNames { get; set; }
        public int port { get; set; }

        public string Ips => ipAddress != null && ipAddress.Count > 0 ? string.Join(" ", ipAddress) : string.Empty;
        public string HostNames => databaseHostNames != null && databaseHostNames.Count > 0 ? string.Join(" ", databaseHostNames) : string.Empty;

        #region Hosts
        public string autoInjection { get; set; }
        public string monitoringMode { get; set; }
        public string osArchitecture { get; set; }
        public string osVersion { get; set; }
        public string state { get; set; }
        #endregion
    }

    public class FromRelationships
    {
        public List<Call> calls { get; set; }
        public List<RunsOnHost> runsOnHost { get; set; }
    }

    public class RunsOnHost
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Call
    {
        public string id { get; set; }
        public string type { get; set; }
    }
}
