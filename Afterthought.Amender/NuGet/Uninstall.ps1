param($installPath, $toolsPath, $package, $project)

#-----------------------------------------------------------------------------
#
# Install and uninstall scripts contributed by Ed Obeda
# Automates the addition and removal of the post-build step to call Afterthought
#
#-----------------------------------------------------------------------------

# Get the current Post Build Event cmds
$currentPostBuildCmds = $project.Properties.Item("PostBuildEvent").Value

$buildCmd = 'Afterthought.Amender "$(TargetPath)"' 

# Check to see if we need to delete anything
if ($currentPostBuildCmds.Contains($buildCmd))
{
    $lines = [regex]::Split($currentPostBuildCmds,"\r\n")
    $cleanedCmds = ""
   
   # Walk each entry
   foreach($line in $lines)
   {
       if( $line -ne $buildCmd) # skip adding ourselves
       {
            if( $line.Length -gt 0) # don't include empty lines
            {
                $cleanedCmds += $line + "`r`n"  # Creating a cleaned list of commands
            }
       }
   }

  # Update the build event with the cleaned values
  $project.Properties.Item("PostBuildEvent").Value = $cleanedCmds
}

## site throws error within VS $project.DTE.ItemOperations.Navigate($package.ProjectUrl)  