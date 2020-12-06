using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.CommonTools.SessionExtension
{
    public static class SessionExtension
    {
        //Session'imizi belirleyecek metodumuzu yaratiyoruz.
        //Session'i extension haline getirmemizin nedeni kompleks tiplerimizi almasi gerektigindendir.
        //Extension Metotlar: Extensionlarimiz sadece statik sinif icerisinde ve generic olmayan siniflarda barinir. Genisletilebilir bir metottur. Bize belirli nesneler icin islemler yapmamizi saglayan metotlar yazmamizi saglar.
        //
        public static void SetObject(this ISession session,string key,object value) {
            string objectString = JsonConvert.SerializeObject(value); //Gelen classi stringe json a cevirme
            session.SetString(key, objectString);
        }

        //Session'i geri almak lazim... Generic metotlar
        //Geriye dondurecegi degerin tipini bilmedigim icin T veriyorum.
        public static T GetObject<T>(this ISession session, string key) where T : class //T Bir referans tip olmak zorundadir.
        {
            string objectString = session.GetString(key);
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T deserializedObject = JsonConvert.DeserializeObject<T>(objectString);
            return deserializedObject;
        }

    }
}
