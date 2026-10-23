using Perfect.IO;
using Perfect.Marshal;

public interface IProtocolData<T> where T : IMarshal
{
    void ReceiveData(T _data);

    void Resolve();
}
