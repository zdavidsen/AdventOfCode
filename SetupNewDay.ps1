Param(
  [int]$Day = (Get-Date).Day,
  [int]$Year = (Get-Date).Year
)

$projDir = Get-ChildItem -Directory -Filter *$Year | select -First 1
$exampleDir = ($projDir.GetDirectories("Examples") | select -First 1).FullName
$inputDir = ($projDir.GetDirectories("Inputs") | select -First 1).FullName

$newChallenge = New-Item "$($projDir.FullName)\Day$Day.cs"

$templateChallenge = Get-ChildItem -Path "AoC_Runner" -File -Filter TemplateChallenge.cs | select -First 1 | Get-Content | Where-Object -FilterScript { -not $_.StartsWith("#") }
$templateChallenge -replace '\$DAY\$', $Day -replace '\$YEAR\$',$Year >> $newChallenge

$templateExample = Get-ChildItem -Path "AoC_Runner" -File -Filter TemplateExample.txt | select -First 1

cp $templateExample.FullName "$exampleDir\Day$Day-Part1.txt"
cp $templateExample.FullName "$exampleDir\Day$Day-Part2.txt"

$newInput = New-Item "$inputDir\Day$Day.txt"

$session = New-Object Microsoft.Powershell.Commands.WebRequestSession
$cookie = New-Object System.Net.Cookie
$cookie.Name = "session"
$cookie.Value = $(./cookie.ps1)
$cookie.Domain = ".adventofcode.com"
$session.Cookies.Add($cookie)

$response = Invoke-WebRequest "https://adventofcode.com/$Year/day/$Day/input" -WebSession $session

$response.Content.Trim() | Set-Content $newInput -NoNewLine

