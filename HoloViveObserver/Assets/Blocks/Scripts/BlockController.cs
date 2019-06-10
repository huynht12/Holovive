using UnityEngine;
using UnityEngine.Networking;

public class BlockController : NetworkBehaviour
{
    private enum State
    {
        Static,
        Placing,
    }

    private Material staticMaterial;
    public Material placingMaterial;
	public SteamVR_TrackedController leftController;
	public SteamVR_TrackedController rightController;
	public int side;// = GameObject.Find("Block Controller").GetComponent<BlockController>().side;
	public Vector3 position;
	public Quaternion rotation;
	//public Transform Controller;
	//private int limp;

    [SyncVar]
    private State state = State.Static;

    private MeshRenderer meshRenderer;

    private Vector3 oldPosition;
    private Vector3 oldScale;
	private Quaternion oldRotation;

    public override void OnStartClient()
    {
        base.OnStartClient();

		//Controller = GameObject.FindGameObjectWithTag ("LeftController").transform;

        if (isClient)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            staticMaterial = meshRenderer.material;
			////this.transform.localScale = new Vector3 (0.035, 0.035, 0.5);
			//this.transform.parent = Controller.transform;
			//this.transform.position = Controller.transform.position;
			//this.transform.rotation = Controller.transform.rotation;
            UpdateMaterial();
        }

        if (isServer)
        {
            SendCurrentPosition();
        }
    }

    private void Update()
    {
        if (isServer)
        {
            SendCurrentPosition();
        }

        if (isClient)
        {
            UpdateMaterial();
        }
    }

    [Server]
    void SendCurrentPosition()
    {
		//var postion;
		if (side % 2 == 0) {
			position = leftController.transform.position;
			rotation = leftController.transform.rotation;
		} 
		else {
			position = rightController.transform.position;
			rotation = rightController.transform.rotation;
		}
		//var position = transform.localPosition;
        var scale = transform.localScale;

		//if (position == oldPosition && scale == oldScale) return;
		if (position == oldPosition && scale == oldScale && rotation == oldRotation) return;

        oldPosition = position;
        oldScale = scale;
		oldRotation = rotation;
        
			RpcUpdatePosition(position, rotation, scale);
    }

    [ClientRpc]
	void RpcUpdatePosition(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.transform.localPosition = position;
        this.transform.localScale = scale;
		this.transform.localRotation = rotation;
    }

    [ServerCallback]
    public void StartPlacing()
    {
        if (state != State.Static)
        {
            return;
        }

        state = State.Placing;
        RpcUpdateMaterial();
    }

    [ServerCallback]
    public void FinishPlacing()
    {
        if (state != State.Placing)
        {
            return;
        }

        state = State.Static;
        RpcUpdateMaterial();
    }

    [ClientRpc]
    private void RpcUpdateMaterial()
    {
        UpdateMaterial();
    }

    [Client]
    private void UpdateMaterial() {
        if (!meshRenderer)
        {
            return;
        }

        switch (state)
        {
            case State.Static:
                meshRenderer.material = staticMaterial;
                break;
            case State.Placing:
                meshRenderer.material = placingMaterial;
                break;
        }
    }
}