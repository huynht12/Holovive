﻿using UnityEngine;
using UnityEngine.Networking;

public class BlockManager : NetworkBehaviour
{
    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;
    public GameObject cubeContainer;
    public GameObject cubeAsset;
	public int side = 0;

    [SyncVar]
    private bool placingCube = false;
    private GameObject currentCube = null;

    public override void OnStartClient()
    {
        base.OnStartClient();

        leftController.TriggerClicked += TriggerClicked;
        rightController.TriggerClicked += TriggerClicked;
        leftController.TriggerUnclicked += TriggerUnclicked;
        rightController.TriggerUnclicked += TriggerUnclicked;
    }
    
    void Update()
    {
        if (isClient && placingCube)
        {
            UpdateCubePosition();
        }
    }

    [ClientCallback]
    private void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (placingCube)
        {
            CmdFinishPlacingCube();
        }
    }

    [ClientCallback]
    private void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (AreBothTriggersPressed() && !placingCube)
        {
            CmdStartPlacingCube();
        }
    }
    
    [Client]
    private bool AreBothTriggersPressed()
    {
        return leftController.triggerPressed && rightController.triggerPressed;
    }

    [Command]
    private void CmdStartPlacingCube()
    {
        currentCube = Instantiate(cubeAsset, cubeContainer.transform);
        currentCube.GetComponent<BlockController>().StartPlacing();
        UpdateCubePosition();
        NetworkServer.Spawn(currentCube);
        RpcSetBlockParent(currentCube);

        placingCube = true;
    }

    [ClientRpc]
    private void RpcSetBlockParent(GameObject cube)
    {
        cube.transform.SetParent(cubeContainer.transform, false);
    }

    [Command]
    private void CmdFinishPlacingCube()
    {
        currentCube.GetComponent<BlockController>().FinishPlacing();
        currentCube = null;

        placingCube = false;
		side += 1;
    }

    [ClientCallback]
    private void UpdateCubePosition()
    {
        Vector3 left = leftController.transform.position;
        Vector3 right = rightController.transform.position;
		Quaternion leftRotation = leftController.transform.rotation;
		Quaternion rightRotation = rightController.transform.rotation;
		if (side % 2 == 0) {
			CmdUpdateCubePosition (left, leftRotation, new Vector3 (0.05f, 0.05f, 0.5f));
			//side += 1;
		}
		else {
			CmdUpdateCubePosition (right, rightRotation, new Vector3 (0.05f, 0.05f, 0.5f));
			//side += 1;
		}
			
        //CmdUpdateCubePosition((left + right) / 2, right - left);
    }

    [Command]
	private void CmdUpdateCubePosition(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!currentCube) return;
		currentCube.transform.position = position;//cubeContainer.transform.position;
		currentCube.transform.localScale = scale;//new Vector3 (0.05f, 0.05f, 0.5f);
		currentCube.transform.localRotation = rotation;

    }
}