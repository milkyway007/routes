namespace Domain
{
    public class RouteBuilder
    {
        private readonly Route _route = new Route();

        private RouteBuilder()
        {
        }

        public static RouteBuilder Init()
        {
            return new RouteBuilder();
        }

        public Route Build() => _route;

        public RouteBuilder SetSourceId(string value)
        {
            if (!int.TryParse(
                value,
                out int parsed))
            {
                _route.SourceId = null;

                return this;
            }

            if (parsed < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _route.SourceId = parsed;

            return this;
        }

        public RouteBuilder SetSourceIata(string value)
        {
            _route.SourceIata = value;

            return this;
        }

        public RouteBuilder SetDestinationId(string value)
        {
            if (!int.TryParse(
                value,
                out int parsed))
            {
                _route.DestinationId = null;

                return this;
            }

            if (parsed < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _route.DestinationId = parsed;

            return this;
        }

        public RouteBuilder SetDestinationIata(string value)
        {
            _route.DestinationIata = value;

            return this;
        }

        public RouteBuilder SetStopCount(string value)
        {
            if (!int.TryParse(
                value,
                out int parsed))
            {
                throw new InvalidOperationException();
            }

            if (parsed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _route.StopCount = parsed;

            return this;
        }
    }
}
