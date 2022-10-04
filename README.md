
# Förarprov Booking 1.0

A simple bot to find the earliest available time and easily spot when there's a cancelation. The bot right now doesn't automatically book the earliest time now because I already have a driver's license so I don't have any SSN to use to build it so it automatically books the time found.

## Settings
To use the program change the App.Config file

Change the value of each key to your own preference

Example

userSSN value="YYYYMMDDXXXX" - Your SSN in format YYYYMMDDXXXX

userLicense value="B" - Your desiered drivers license for exmapel B for Bil/Car license

userTestType value="" - Either "Körprov" or "Kunskapsprov"

userCity value="" - City where you want to book the test. Check https://fp.trafikverket.se/Boka/#/licence for full list of aviable cities.

userCar value="" - Either "Manuell bil" or "Automatbil"

userDates value="" - Your desired date to check if time is available. You can use multiple dates by separating them with ",". 

refreshTime value="" - How often the bot should check if a new time gets available. Time is in seconds so 120 = every 2 minutes. Minimum is 120

		<add key="userSSN" value ="YYYYMMDDXXXX" /> 
		<add key="userLicense" value ="B" />
		<add key="userTestType" value ="Körprov" />
		<add key="userCity" value="Avesta"/>
		<add key="userCar" value="Manuell bil"/>
		<add key="userDates" value ="2022-10-03,2022-10-04,2022-10-05,2022-10-06,2022-10-07,2022-10-08,2023-02-09"/>
		<add key ="refreshTime" value="120"/>


### You can also use the tool provided here to create the config file 
#### Available soon

## Upcoming

### Automatically book time
### Email and or SMS notification when time found
