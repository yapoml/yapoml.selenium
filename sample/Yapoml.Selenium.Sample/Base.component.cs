namespace Yapoml.Selenium.Sample;

partial class BaseComponent<TComponent, TConditions, TCondition>
{
    partial class Conditions<TSelf>
    {
        public TSelf IsNotWhite()
        {
            using (this.Logger.BeginLogScope($"Expect {this.ElementHandler.ComponentMetadata.Name} color is not white"))
            {
                return Styles.BackgroundColor.DoesNotContain("255, 255, 255");
            }
        }
    }
}
