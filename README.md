# RemoveEmptyXMLNodes
A library to parse xml files and remove the empty xml nodes.

The code uses state models to make a single pass through the XML to jump from state to state based on the input characters.
We are extending the existing StringWriter to perform our checks and modify the Xml.

Added basic version of Remove Xml class which looks for char based on position and not states.
Added state based version of Remove Xml class.
//REfactoring of state based version pending
//Update diagram and info