using System.Collections;
using Perfect.IO;

public interface IModule
{
    void Init();

    void LoadConfigDone();

    void Login();

    void LoginDone();

    void Logout();

    void Dispose();
}
