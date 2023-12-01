Param(
  [int]$Day = (Get-Date).Day,
  [int]$Year = (Get-Date).Year
)
$dayStr = "{0:D2}" -f $Day

$projDir = Get-ChildItem -Directory -Filter *$Year | select -First 1
$exampleDir = Join-Path $projDir.FullName "Examples"
$inputDir = Join-Path $projDir.FullName "Inputs"

if (-NOT (Test-Path $exampleDir))
{
  mkdir $exampleDir
}
if (-NOT (Test-Path $inputDir))
{
  mkdir $inputDir
}

$newChallenge = New-Item "$($projDir.FullName)\Day$dayStr.cs" 2>&1 | Out-Null

if ($null -NE $newChallenge)
{
  $templateChallenge = Get-ChildItem -Path "AoC_Runner" -File -Filter TemplateChallenge.cs | select -First 1 | Get-Content | Where-Object -FilterScript { -not $_.StartsWith("#") }
  $templateChallenge -replace '\$DAY\$',$Day -replace '\$YEAR\$',$Year >> $newChallenge
}

$templateExample = Get-ChildItem -Path "AoC_Runner" -File -Filter TemplateExample.txt | select -First 1

Copy-Item $templateExample.FullName "$exampleDir\Day$dayStr-Part1.txt" 2>&1 | Out-Null
Copy-Item $templateExample.FullName "$exampleDir\Day$dayStr-Part2.txt" 2>&1 | Out-Null

$newInput = New-Item "$inputDir\Day$dayStr.txt" 2>&1 | Out-Null
if ($null -NE $newInput)
{
  $session = New-Object Microsoft.Powershell.Commands.WebRequestSession
  $cookie = New-Object System.Net.Cookie
  $cookie.Name = "session"
  $cookie.Value = $(./cookie.ps1)
  $cookie.Domain = ".adventofcode.com"
  $session.Cookies.Add($cookie)

  $response = Invoke-WebRequest "https://adventofcode.com/$Year/day/$Day/input" -WebSession $session

  $response.Content.Trim() | Set-Content $newInput -NoNewLine
}

