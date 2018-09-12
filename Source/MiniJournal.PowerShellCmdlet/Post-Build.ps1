param(
	[Parameter()] $ProjectName,
	[Parameter()] $ConfigurationName,
	[Parameter()] $TargetDir
)

Copy '*' '.\MiniJournal.PowerShellCmdlet' -Force -Verbose
