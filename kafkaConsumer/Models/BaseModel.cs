namespace kafkaConsumer.Models
{
    public class BaseModel<T>
    {

            public Schema schema { get; set; }
            public Payload<T> payload { get; set; }

        public class Schema
        {
            public string type { get; set; }
            public Field[] fields { get; set; }
            public bool optional { get; set; }
            public string name { get; set; }
        }

        public class Field
        {
            public string type { get; set; }
            public SubField[] fields { get; set; }
            public bool optional { get; set; }
            public string name { get; set; }
            public string field { get; set; }
        }

        public class SubField
        {
            public string type { get; set; }
            public bool optional { get; set; }
            public string field { get; set; }
            public string name { get; set; }
            public int version { get; set; }
            public Parameters parameters { get; set; }
            public string _default { get; set; }
        }

        public class Parameters
        {
            public string allowed { get; set; }
        }

        public class Payload<T>
        {
            public T before { get; set; }
            public T after { get; set; }
            public Source source { get; set; }
            public string op { get; set; }
            public long ts_ms { get; set; }
            public object transaction { get; set; }
        }

        public class Source
        {
            public string version { get; set; }
            public string connector { get; set; }
            public string name { get; set; }
            public long ts_ms { get; set; }
            public string snapshot { get; set; }
            public string db { get; set; }
            public object sequence { get; set; }
            public string schema { get; set; }
            public string table { get; set; }
            public object change_lsn { get; set; }
            public string commit_lsn { get; set; }
            public object event_serial_no { get; set; }
        }
    }
}