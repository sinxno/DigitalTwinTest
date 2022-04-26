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

$sql = "SELECT InstrumentName, UNIX_TIMESTAMP(Timestamp), ValueRaw, ValueScaled, AL, AH FROM Measurement ORDER BY Timestamp DESC";

echo '<table cellspacing="5" cellpadding="5">
	<tr>
	<td>InstrumentName</td>
	<td>UNIX_TIMESTAMP</td>
	<td>ValueRaw</td>
	<td>ValueScaled</td>
	<td>AL</td>
	<td>AH</td>
	</tr>';

if ($result = $conn->query($sql)) {
	while ($row = $result->fetch_assoc()) {
		$row_instrumentName = $row["InstrumentName"];
		$row_timestamp = $row["UNIX_TIMESTAMP(Timestamp)"];
		$row_valueRaw = $row["ValueRaw"];
		$row_valueScaled = $row["ValueScaled"];
		$row_al = $row["AL"];
		$row_ah = $row["AH"];

		echo '<tr>
			<td>' . $row_instrumentName . '</td>
			<td>' . $row_timestamp . '</td>
			<td>' . $row_valueRaw . '</td>
			<td>' . $row_valueScaled . '</td>
			<td>' . $row_al . '</td>
			<td>' . $row_ah . '</td>
			</tr>';
	}
	$result->free();
}

$conn->close();
?>
</table>
</body>
</html>
