namespace XR_Gestures
{
    public class WithinDistance : FunctionObject
    {

        TrackingSession? session;

        public float maxDistance;

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

