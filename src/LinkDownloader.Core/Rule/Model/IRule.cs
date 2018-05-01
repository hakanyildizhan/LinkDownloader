namespace LinkDownloader.Core.Rule.Model
{
	public interface IRule<Link>
	{
		/// <summary>
		/// Evaluates the validity of a rule given an instance of an entity
		/// </summary>
		/// <param name="entity">Entity to evaluate</param>
		/// <returns>result of the evaluation</returns>
		bool Run(Link link);

		/// <summary>
		/// Whether the rule is applicable on the input
		/// </summary>
		bool IsApplicable { get; set; }

		/// <summary>
		/// The unique indentifier for a rule.
		/// </summary>
		int RuleId { get; set; }

		/// <summary>
		/// Common name of the rule, not unique
		/// </summary>
		string RuleName { get; set; }

		/// <summary>
		/// Indicates the message used to notify the user if the rule fails
		/// </summary>
		string ValidationMessage { get; set; }

		/// <summary>
		/// indicator of whether the rule is enabled or not
		/// </summary>
		bool IsEnabled { get; set; }

		/// <summary>
		/// Represents the order in which a rule should be executed relative to other rules
		/// </summary>
		int SortOrder { get; set; }

	}
}
