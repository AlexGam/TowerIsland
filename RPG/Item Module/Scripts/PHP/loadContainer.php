<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$player = mysql_real_escape_string($_POST['name']);
	$containerId = mysql_real_escape_string($_POST['id']);

	$sql = mysql_query("SELECT item FROM container WHERE player = '$player' AND containerid = '$containerId'");
	$row = mysql_fetch_row($sql);

 	echo $row[0];
	mysql_close($db);
?> 