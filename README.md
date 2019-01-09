# RemoveEmptyXMLNodes
A library to parse xml files and remove the empty xml nodes.

The code uses state models to make a single pass through the XML to jump from state to state based on the input characters.
We are extending the existing StringWriter to perform our checks and modify the Xml.

//Update diagram and info