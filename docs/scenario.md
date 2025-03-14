# Scenario

A regional environmental agency has tasked you with developing a cross-platform application to enhance their monitoring of local environmental conditions. The agency is focused on monitoring air quality, water quality, and weather conditions to ensure public safety and adherence to environmental regulations.

The agency utilises a widespread network of sensors that collect data on air pollutants, water pH levels, and meteorological data. This data is vital for the agency to make informed decisions and effectively address environmental issues.

There are three categories of environmental data to be monitored:

- Air Quality: Requires real-time monitoring and analysis to identify trends or sudden changes.
- Water Quality: Continuous updates and detailed reports on chemical and biological content are necessary.
- Weather Conditions: Live tracking and forecasting are essential to anticipate and respond to environmental changes.

An initial requirements elicitation session has identified the following user stories that the application should include:

As an Environmental Scientist, I want to

- Manage sensor accounts and configure settings. <-- Add setting menu for each sensor, add fake settings
- View and analyse historical environmental data. <-- Graph the data
- Receive real-time alerts on threshold breaches, displayed on an interactive map.
- Generate comprehensive reports on environmental trends.
- Use a map to view real-time sensor statuses and alerts geographically. <-- Map with sensors on it
- Locate and navigate to sensors in the field for maintenance or inspection purposes.

As an Operations Manager I want to

- Monitor the operational status of sensors. <-- Add battery level to map
- Schedule maintenance and ensure timely checks. <-- Battery low alerts
- Verify the accuracy and integrity of collected data. <-- Graph the data with colours for things that may be anomalies
- Address and report sensor malfunctions or anomalies.

As an Administrator I want to

- Manage user access and roles within the application. <-- Database accounts for each user
- Maintain high levels of system security and data protection. <-- Minimum required permissions on database
- Update sensor configurations and firmware.
- Oversee data storage and implement backup strategies. <-- Backup button which copies database file