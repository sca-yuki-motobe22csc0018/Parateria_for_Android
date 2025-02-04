// �T�[�r�X���P�[�^�[
public static class Locator<T> where T : class
{
    public static T Instance { get; private set; }

    public static bool IsValid() => Instance != null; // �L�����ǂ���

    // Instance�̒l��ݒ肷��
    public static void Bind(T instance)
    {
        Instance = instance;
    }

    // Instance�̒l��null�ɂ���
    public static void Unbind(T instance)
    {
        if (Instance == instance)
        {
            Instance = null;
        }
    }

    // �����I��null�ɂ���
    public static void Clear()
    {
        Instance = null;
    }

    /* �g�p���@ */
    /*
    Awake(),OnEnable()����
    Locator<T>.Bind(this);�ŃC���X�^���X�o�^
    
    OnDestroy(),OnDisable()����
    Locator<T>.Unbind(this);�ŉ���

    public virtual void Hoge(T hoge){}


    �֐������s���鎞��
    Locator<T>.Instance.Hoge(hoge);
    */
}