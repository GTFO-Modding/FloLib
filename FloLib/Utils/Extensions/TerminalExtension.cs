using LevelGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils.Extensions;
/// <summary>
/// Extension Methods related to <see cref="LG_ComputerTerminal"/>
/// </summary>
public static class TerminalExtension
{
    /// <summary>
    /// Set Terminal Powered or not.
    /// Un-Powering terminal will disable interaction, screen and kick-out the player while using it
    /// </summary>
    /// <param name="terminal">Target Terminal</param>
    /// <param name="isPowered">Powered Info</param>
    public static void SetPowered(this LG_ComputerTerminal terminal, bool isPowered)
    {
        // help terminals not brick
        terminal.OnProximityExit();

        //Disable Interaction
        var interact = terminal.GetComponentInChildren<Interact_ComputerTerminal>(includeInactive: true);
        if (interact != null)
        {
            interact.enabled = isPowered;
            interact.SetActive(isPowered);
        }

        //Disable Terminal Screen Completely
        var guixSceneLink = terminal.GetComponent<GUIX_VirtualSceneLink>();
        if (guixSceneLink != null && guixSceneLink.m_virtualScene != null)
        {
            var virtCam = guixSceneLink.m_virtualScene.virtualCamera;
            var nearClip = isPowered ? 0.3f : 0.0f;
            var farClip = isPowered ? 1000.0f : 0.0f;
            virtCam.SetFovAndClip(virtCam.paramCamera.fieldOfView, nearClip, farClip);
        }

        //Disable Terminal Text
        if (terminal.m_text != null)
        {
            terminal.m_text.enabled = isPowered;
        }

        //Exit the Terminal Interaction if it was using
        if (!isPowered)
        {
            var interactionSource = terminal.m_localInteractionSource;
            if (interactionSource != null && interactionSource.FPItemHolder.InTerminalTrigger)
            {
                terminal.ExitFPSView();
            }
        }
    }
}
