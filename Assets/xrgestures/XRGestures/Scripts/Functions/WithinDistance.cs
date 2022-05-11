namespace XR_Gestures
{
    public class WithinDistance : FunctionObject
    {

        TrackingSession? session;

        public float maxDistance;

        Tracker mainTracker;
        public override void Initialise(FunctionArgs _args)
        {
            mainTracker = _args.mainTracker;
        }
        public override Output Run(Output res)
        {
            session = res.session;

            return base.Run(res);
        }

        protected override bool Function()
       => session == null ? false
               : (session.Value.WorldStartPos - mainTracker.Position).magnitude < maxDistance;
    }

}

