using NaughtyAttributes;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
public class AnimationRecorder : MonoBehaviour
{
    GameObjectRecorder _recorder;

    #region Inspector Variables

    [Tooltip("Must start with Assets/")]
    [SerializeField] string _saveFolderLocation = "Assets/";

    [SerializeField] string _clipName;
    [SerializeField] float _frameRate = 15f;

    [SerializeField] GameObject _gameObject;
    #endregion

    private AnimationClip _clip;
    private AnimationClip _currentClip;
    private bool _canRecord = true;
    private int _index;
    private string _currentClipName;


    private void OnEnable()
    {
        if (_clip == null)
        {
            CreateNewClip();
        }

        var savedIndex = PlayerPrefs.GetInt(gameObject.name + "Index");

        if (savedIndex != 0)
        {
            _index = savedIndex;
        }
    }

    void Start()
    {
        _recorder = new GameObjectRecorder(gameObject);
        _recorder.BindComponentsOfType<Transform>(gameObject, true);

        if (_clipName == "")
        {
            _clipName = gameObject.name + "_animation";
        }

    }

    [Button]
    private void StartRecording()
    {
        _canRecord = true;
        CreateNewClip();
        Debug.Log("Animation Recording for " + gameObject.name + " has STARTED");
    }

    [Button]
    private void StopRecording()
    {
        Debug.Log("Animation Recording for " + gameObject.name + " has STOPPED");

        _canRecord = false;

        _recorder.SaveToClip(_currentClip);

        AssetDatabase.CreateAsset(_currentClip, _saveFolderLocation + _currentClipName + ".anim");

        AssetDatabase.SaveAssets();
    }


    private void LateUpdate()
    {
        if (_clip == null)
        {
            return;
        }

        if (_canRecord)
        {
            _recorder.TakeSnapshot(Time.deltaTime);
        }
    }

    private void CreateNewClip()
    {
        _clip = new AnimationClip();

        if (_clip.name.Contains(_clip.name))
        {
            _clip.name = _clipName + " " + (_index++);
            _currentClipName = _clip.name;
        }

        _clip.frameRate = _frameRate;

        _currentClip = _clip;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(gameObject.name + "Index", _index);
    }

}