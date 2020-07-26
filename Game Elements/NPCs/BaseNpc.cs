
[System.Serializable]
public class BaseNpc
{
    public string name;
    public int id;
    public bool canSpeak;
    public bool finishSpeak;
    public Dialog[] dialogs;
    int index;
    public bool questNpc;

    public Dialog NextChat()
    {
        if (dialogs.Length > 0)
        {
            if (index < dialogs.Length)
            {
                index++;
                if (index == dialogs.Length)
                {
                    finishSpeak = true;
                }
                return dialogs[index - 1];
            }
            else
            {
                index = 1;
                if (index == dialogs.Length)
                {
                    finishSpeak = true;
                }
                return dialogs[0];
            }
        }
        else
        {
            return null;
        }
    }

    public Dialog ReturnFirstDialog()
    {
        if (dialogs.Length > 0)
        {
            return dialogs[0];
        }
        else
        {
            return null;
        }
    }
}