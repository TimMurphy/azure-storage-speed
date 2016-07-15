namespace AzureStorageSpeed.Specifications.Helpers
{
    public abstract class CommonSteps
    {
        protected readonly Actual Actual;
        protected readonly Given Given;

        protected CommonSteps(Given given, Actual actual)
        {
            Given = given;
            Actual = actual;
        }
    }
}