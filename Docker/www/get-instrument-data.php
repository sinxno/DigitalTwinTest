<!DOCTYPE html>
<html><body>

<?php

$servername = "docker-db-1";

$dbname = "SoftSensConf";

$username = "unity";

$password = "Password1";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT Name, LRV, URV, AL_Value, AH_Value FROM Instrument ORDER BY Name DESC";

echo '<table cellspacing="5" cellpadding="5">
	<tr>
	<td>Name</td>
	<td>LRV</td>
	<td>URV</td>
	<td>AL_Value</td>
	<td>AH_Value</td>
	</tr>';

if ($result = $conn->query($sql)) {
	while ($row = $result->fetch_assoc()) {
		$row_name = $row["Name"];
		$row_lrv = $row["LRV"];
		$row_urv = $row["URV"];
		$row_al_value = $row["AL_Value"];
		$row_ah_value = $row["AH_Value"];

		echo '<tr>
			<td>' . $row_name . '</td>
			<td>' . $row_lrv . '</td>
			<td>' . $row_urv . '</td>
			<td>' . $row_al_value . '</td>
			<td>' . $row_ah_value . '</td>
			</tr>';
	}
	$result->free();
}

$conn->close();
?>
</table>
</body>
</html>
