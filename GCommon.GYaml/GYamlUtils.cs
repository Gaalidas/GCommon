using GCommon.Collections;
using GCommon.GYaml.Enums;

namespace GCommon.GYaml
{
	public static class GYamlUtils
	{
		public static char[] NodeBrackets = new char[] { '{', '}' };

		public static bool TryParse(this GYamlDocument input)
		{
			GList<GValueSet<string, GYamlNodeType>> NodeTypesList = new GList<GValueSet<string, GYamlNodeType>>
			{
				new GValueSet<string, GYamlNodeType>("Block", GYamlNodeType.Block),
				new GValueSet<string, GYamlNodeType>("Child", GYamlNodeType.Child),
				new GValueSet<string, GYamlNodeType>("Item", GYamlNodeType.Item),
				new GValueSet<string, GYamlNodeType>("Entity", GYamlNodeType.Entity),
				new GValueSet<string, GYamlNodeType>("Template", GYamlNodeType.Template),
				new GValueSet<string, GYamlNodeType>("Container", GYamlNodeType.Container),
				new GValueSet<string, GYamlNodeType>("LootGroup", GYamlNodeType.LootGroup)
			};

			if (input.Lines.Count == 0)
				return false;

			bool MadeNode = false;
			GYamlNode newNode = new GYamlNode(GYamlNodeType.Unknown);

			foreach (string line in input.Lines)
			{
				string lineTemp = line;

				if (lineTemp.StartsWith(NodeBrackets[0]) && !MadeNode)
				{
					newNode = new GYamlNode(GYamlNodeType.Unknown);
					newNode.Lines.Add(line);
					lineTemp = lineTemp.TrimStart(NodeBrackets[0]).Replace(" ", string.Empty);

					for (int i = 0; i < NodeTypesList.Count; i++)
					{
						if (lineTemp.StartsWith(NodeTypesList[i].Val1))
						{
							newNode.NodeType = NodeTypesList[i].Val2;

							if (lineTemp.RemoveFirst(NodeTypesList[i].Val1, out string outStr))
							{
								lineTemp = outStr.TrimStart(' ');

								if (lineTemp.ToLower().StartsWith("id:") && lineTemp.TrySplit(": ", out string[] tempIDSplit) && tempIDSplit.Length == 2)
									newNode.NodeID = int.Parse(tempIDSplit[1]);
								else
								{
									// To-Do: Handle cases where ID is not the next item in the line.
								}
							}
						}
					}
				}
				else
				{
					if (MadeNode && newNode.NodeType != GYamlNodeType.Unknown)
					{

					}
				}
			}

			return input.Top.Lines.Count > 0;
		}
	}
}