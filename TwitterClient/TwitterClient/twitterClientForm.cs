///ETML
///Auteur : Alison Savary
///Date : 16.03.2017
///Description : Client Twitter en C#
///   
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace TwitterClient
{
    public partial class twitterClientForm : Form
    {

        // Texte des messages box
        private const string STR_QUIT_BOX_MESSAGE = "Voulez-vous vraiment quitter ?";
        private const string STR_QUIT_BOX_TITLE = "Fermeture du programme";
        private const string STR_RETWEET_BOX_TITLE = "Retweet";
        private const string STR_RETWEET_BOX_MESSAGE = "Vous avez retweeté le tweet identifié par l'id {0}";
        private const string STR_UNRETWEET_BOX_MESSAGE = "Vous ne retweetez plus le tweet identifié par l'id {0}";
        private const string STR_LIKE_BOX_TITLE = "Aimer un tweet";
        private const string STR_LIKE_BOX_MESSAGE = "Vous avez aimé le tweet identifié par l'id {0}";
        private const string STR_UNLIKE_BOX_MESSAGE = "Vous n'aimez plus le tweet identifié par l'id {0}";
        private const string STR_SAVE_BOX_TITLE = "Sauvegarde";
        private const string STR_SAVE_BOX_MESSAGE = "Sauvegarde des tweets efféctuée";

        // OAuth
        private const string STR_OAUTH_SIGNATURE_METHODE = "HMAC-SHA1";
        private const string STR_OAUTH_VERSION = "1.0";
        private string strOauthConsumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerKey"];
        private string strOauthConsumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecret"];
        private string strOauthAccessToken = System.Configuration.ConfigurationManager.AppSettings["accessToken"];
        private string strOauthAccessTokenSecret = System.Configuration.ConfigurationManager.AppSettings["accessTokenSecret"];

        private string strLastClickedTweetID; // ID du dernier tweet aimé ou de la timeline cliqué
        private int intLastClickedTweetIndex; // Index correspondant à l'index du tableau du dernier tweet cliqué

        // URLs - POST
        private string strPostTweetUrl = "https://api.twitter.com/1.1/statuses/update.json";
        private string strPostLikeTweetUrl = "https://api.twitter.com/1.1/favorites/create.json";
        private string strPostUnlikeTweetUrl = "https://api.twitter.com/1.1/favorites/destroy.json";
        private string strPostRetweetUrl = "https://api.twitter.com/1.1/statuses/retweet/{0}.json";
        private string strPostUnretweetUrl = "https://api.twitter.com/1.1/statuses/unretweet/{0}.json";

        // URLs - GET 
        private string strGetTweetUrl = "https://api.twitter.com/1.1/statuses/user_timeline.json";
        private string strGetFollowingsUrl = "https://api.twitter.com/1.1/friends/list.json";
        private string strGetFollowersUrl = "https://api.twitter.com/1.1/followers/list.json";
        private string strGetTimelineUrl = "https://api.twitter.com/1.1/statuses/home_timeline.json";
        private string strGetLikesUrl = "https://api.twitter.com/1.1/favorites/list.json";

        // ID Twitter de l'utilisateur (@ProjetTCSharp)
        private string strUserId = "842374519855685633";

        // Tableaux de label pour l'affichage des données récupérées
        private Label[] tab_tweetsLabel = new Label[20]; // Derniers tweets postés
        private Label[] tab_FollowingsFollowersLabel = new Label[20]; // Abonnements ou abonnés
        private Label[] tab_TimelineLikesLabel = new Label[20]; // Timeline ou les tweets aimés

        // Liste dynamique de PictureBox pour l'affichage des images
        private List<PictureBox> list_tweetImagesPictureBox = new List<PictureBox>();

        // Tableaux JSON pour contenir des tweets
        JArray jaTimelineLikes; // Timeline ou les tweets aimés
        JArray jArrayTweets; // Derniers tweets postés

        // Tableau string pour sauvegarder les tweets dans un fichier texte
        private string[] tab_strSavedTweets = new string[21];

        /// <summary>
        /// Constructeur de la fenêtre 
        /// </summary>
        public twitterClientForm()
        {
            InitializeComponent();
            createLabelsTables();
            findLastTweets();
        }

        /// <summary>
        /// Crée les tableaux de labels permettant d'afficher différentes données
        /// </summary>
        private void createLabelsTables()
        {
            for (int i = 0; i < 20; i++)
            {
                // Tableau de label pour les 20 derniers tweets
                this.tab_tweetsLabel[i] = new Label();
                this.tab_tweetsLabel[i].AutoSize = false;
                this.tab_tweetsLabel[i].Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tab_tweetsLabel[i].Name = "tweetLabel" + i;
                this.tab_tweetsLabel[i].Size = new System.Drawing.Size(225, 130);
                this.tab_tweetsLabel[i].BorderStyle = BorderStyle.FixedSingle;
                this.tab_tweetsLabel[i].Text = "";

                // Tableau de label pour les 20 derniers abonnements et abonnés
                this.tab_FollowingsFollowersLabel[i] = new Label();
                this.tab_FollowingsFollowersLabel[i].AutoSize = false;
                this.tab_FollowingsFollowersLabel[i].Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tab_FollowingsFollowersLabel[i].Name = "followingLabel" + i;
                this.tab_FollowingsFollowersLabel[i].Size = new System.Drawing.Size(480, 20);
                this.tab_FollowingsFollowersLabel[i].BorderStyle = BorderStyle.FixedSingle;
                this.tab_FollowingsFollowersLabel[i].Text = "";

                // Tableau de label pour les 20 derniers tweets de la timeline et tweets aimés
                this.tab_TimelineLikesLabel[i] = new Label();
                this.tab_TimelineLikesLabel[i].AutoSize = false;
                this.tab_TimelineLikesLabel[i].Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tab_TimelineLikesLabel[i].Name = "tweetLabel" + i;
                this.tab_TimelineLikesLabel[i].Size = new System.Drawing.Size(340, 110);
                this.tab_TimelineLikesLabel[i].BorderStyle = BorderStyle.FixedSingle;
                this.tab_TimelineLikesLabel[i].Text = "";
                // Ajout de la méthode Label_Click pour l'événement Click
                this.tab_TimelineLikesLabel[i].Click += new System.EventHandler(this.label_Click);
            }
        }

        /// <summary>
        /// Permet de générer l' oauth_nonce pour l'authentification
        /// http://www.thatsoftwaredude.com/content/6289/how-to-post-a-tweet-using-c-for-single-user
        /// </summary>
        /// <returns>Un string composé aléatoirement de 32 lettres majuscules</returns>
        private string generateNonce()
        {
            string strNonce = string.Empty;

            // Déclaration d'un générateur aléatoire
            Random randomGenerator = new Random();
            int intASCIICode = 0;

            // Crée un string de 32 caractères composé de lettres majuscules
            for (int i = 0; i < 32; i++)
            {
                intASCIICode = randomGenerator.Next(65, 90);
                char charStringPart = Convert.ToChar(intASCIICode);
                strNonce += charStringPart;
            }
            return strNonce;
        }

        /// <summary>
        /// Génére le timestamp de la date transmise en paramètre
        /// </summary>
        /// <param name="date">Date à convertir en timestamp</param>
        /// <returns>Timestamp de la date donnée</returns>
        private double generateTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// Génère le header "Authorizartion" pour la requête http
        /// </summary>
        /// <param name="strParameter">Paramètre faisant parti de la requête http comme un nom d'utilisateur ou le tweet</param>
        /// <returns>Un string contenant le header</returns>
        private string generateAuthorizationHeader(string strParameter, string strRequestType)
        {
            // Genère le oauth_nonce et le oauth_timestamp
            string strNonce = generateNonce();
            double timestamp = generateTimestamp(DateTime.Now);

            string dst = string.Empty;
            // Création du header
            dst += "OAuth ";
            dst += string.Format("oauth_consumer_key=\"{0}\", ", Uri.EscapeDataString(strOauthConsumerKey));
            dst += string.Format("oauth_nonce=\"{0}\", ", Uri.EscapeDataString(strNonce));
            dst += string.Format("oauth_signature=\"{0}\", ", Uri.EscapeDataString(generateOauthSignature(strParameter, strNonce, timestamp.ToString(), strRequestType)));
            dst += string.Format("oauth_signature_method=\"{0}\", ", Uri.EscapeDataString(STR_OAUTH_SIGNATURE_METHODE));
            dst += string.Format("oauth_timestamp=\"{0}\", ", timestamp);
            dst += string.Format("oauth_token=\"{0}\", ", Uri.EscapeDataString(strOauthAccessToken));
            dst += string.Format("oauth_version=\"{0}\"", Uri.EscapeDataString(STR_OAUTH_VERSION));
            return dst;
        }

        /// <summary>
        /// Génére la signature oauth
        /// </summary>
        /// <param name="strParameter">Paramètre faisant parti de la requête http comme un nom d'utilisateur ou le tweet</param>
        /// <param name="strNonce">Chaine de caractère aléatoire générée pour chaque requête</param>
        /// <param name="timestamp">Timestamp de la date de la requête</param>
        /// <returns>La signature oauth sous forme de string</returns>
        private string generateOauthSignature(string strParameter, string strNonce, string timestamp, string strRequestType)
        {
            
            string result = string.Empty; // Elément à hasher et signer
            string dst = string.Empty; // Partie spécifications OAuth à signer en combinant avec l'URL 

            dst += string.Format("oauth_consumer_key={0}&", Uri.EscapeDataString(strOauthConsumerKey));
            dst += string.Format("oauth_nonce={0}&", Uri.EscapeDataString(strNonce));
            dst += string.Format("oauth_signature_method={0}&", Uri.EscapeDataString(STR_OAUTH_SIGNATURE_METHODE));
            dst += string.Format("oauth_timestamp={0}&", timestamp);
            dst += string.Format("oauth_token={0}&", Uri.EscapeDataString(strOauthAccessToken));
            dst += string.Format("oauth_version={0}", Uri.EscapeDataString(STR_OAUTH_VERSION));
            // Selon le type de requête qui sera envoyée, ajoute la dernière ligne correspondante
            switch (strRequestType)
            {
                case "tweet":
                    {
                        dst += string.Format("&status={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "findTweets":
                    {
                        dst += string.Format("&user_id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "findFollowings":
                    {
                        dst += string.Format("&user_id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "findFollowers":
                    {
                        dst += string.Format("&user_id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "findLikes":
                    {
                        dst += string.Format("&user_id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "likeTweet":
                    {
                        dst += string.Format("&id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
                case "unlikeTweet":
                    {
                        dst += string.Format("&id={0}", Uri.EscapeDataString(strParameter));
                        break;
                    }
            }

            // Création de la clé pour la signature
            string signingKey = string.Empty;
            signingKey = string.Format("{0}&{1}", Uri.EscapeDataString(strOauthConsumerSecret), Uri.EscapeDataString(strOauthAccessTokenSecret));
            string oauth_signature;
            // Selon le type de requête, création de la signature correspondante
            switch (strRequestType)
            {
                case "tweet":
                    {
                        result += "POST&";
                        result += Uri.EscapeDataString(strPostTweetUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "findTweets":
                    {
                        result += "GET&";
                        result += Uri.EscapeDataString(strGetTweetUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "findFollowings":
                    {
                        result += "GET&";
                        result += Uri.EscapeDataString(strGetFollowingsUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "findFollowers":
                    {
                        result += "GET&";
                        result += Uri.EscapeDataString(strGetFollowersUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "findTimeline":
                    {
                        result += "GET&";
                        result += Uri.EscapeDataString(strGetTimelineUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "findLikes":
                    {
                        result += "GET&";
                        result += Uri.EscapeDataString(strGetLikesUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "likeTweet":
                    {
                        result += "POST&";
                        result += Uri.EscapeDataString(strPostLikeTweetUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "unlikeTweet":
                    {
                        result += "POST&";
                        result += Uri.EscapeDataString(strPostUnlikeTweetUrl);
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "retweet":
                    {
                        result += "POST&";
                        result += Uri.EscapeDataString(string.Format(strPostRetweetUrl, strLastClickedTweetID));
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                case "unretweet":
                    {
                        result += "POST&";
                        result += Uri.EscapeDataString(string.Format(strPostUnretweetUrl, strLastClickedTweetID));
                        result += "&";
                        result += Uri.EscapeDataString(dst);
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signingKey)))
                        {
                            oauth_signature = Convert.ToBase64String(
                                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(result)));
                        }
                        return oauth_signature;
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        /// <summary>
        /// Envoie la requête http qui permet de poster un tweet et gère les erreurs
        /// </summary>
        /// <param name="strTweet">Tweet à poster</param>
        private void sendTweet(string strTweet)
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strTweet, "tweet");
            // Création du body de la requête
            string strPostBody = "status=" + Uri.EscapeDataString(strTweet);

            WebRequest postRequest = createPostRequest(strAuthHeader, strPostTweetUrl, strPostBody);
            
            try // Permet la gestion des exceptions si le serveur retourne une erreur.
            {
                // Envoi de la requête
                WebResponse postResponse = postRequest.GetResponse();
                postResponse.Close();
            }
            catch (WebException e) // Si une erreur se produit
            {   
                // Récupère le message d'erreur et l'affiche dans une MessageBox
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée une requête http POST avec les paramètres donnés
        /// </summary>
        /// <param name="strAuthHeader">Header d'authentification</param>
        /// <param name="strPostUrl">URL Resource</param>
        /// <param name="strPostBody">Body de la requête</param>
        /// <returns>Requête http POST prête à être envoyée </returns>
        private WebRequest createPostRequest(string strAuthHeader, string strPostUrl, string strPostBody)
        {
            // Création de la requête avec l'url
            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(strPostUrl);
            // Ajout du header
            postRequest.Headers.Add("Authorization", strAuthHeader);
            // Ajout de la méthode, verbe http
            postRequest.Method = "POST";
            postRequest.UserAgent = "OAuth gem v0.4.4";
            // Ajout de l'hôte
            postRequest.Host = "api.twitter.com";
            // Spécification du type de contenu
            postRequest.ContentType = "application/x-www-form-urlencoded";
            postRequest.ServicePoint.Expect100Continue = false;
            postRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            // Ajout du contenu du body de la requête s'il y en a un.
            if (strPostBody.Length > 0)
            {
                using (Stream stream = postRequest.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(strPostBody);
                    stream.Write(content, 0, content.Length);
                }
            }
            return postRequest;
        }

        /// <summary>
        /// Crée une requête http GET avec les paramètres donnés
        /// </summary>
        /// <param name="strAuthHeader">Header d'authentification</param>
        /// <param name="strGetUrl">URL Resource</param>
        /// <returns>Requête http GET prête à être envoyée </returns>
        private WebRequest createGetRequest(string strAuthHeader, string strGetUrl)
        {
            // Création de la requête avec l'url suivi des paramètres
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(strGetUrl);
            // Ajout du header
            getRequest.Headers.Add("Authorization", strAuthHeader);
            // Ajout de la méthode, verbe http
            getRequest.Method = "GET";
            getRequest.UserAgent = "OAuth gem v0.4.4";
            // Ajout de l'hôte
            getRequest.Host = "api.twitter.com";
            // Spécification du type de contenu
            getRequest.ContentType = "application/x-www-form-urlencoded";
            getRequest.ServicePoint.Expect100Continue = false;
            getRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return getRequest;
        }

        /// <summary>
        /// Crée la requête http permettant d'obtenir les 20 derniers tweets postés par l'utilisateur
        /// et l'envoie. Le résultat est obtenu en JSON. Traitement et affichage des informations reçues.
        /// </summary>
        private void findLastTweets()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strUserId, "findTweets");
            // Gestion des paramètres de la requête
            string postbody = "user_id=" + Uri.EscapeDataString(strUserId);

            WebRequest getRequest = createGetRequest(strAuthHeader, strGetTweetUrl + "?" + postbody);

            // Vide la liste de PictureBox qui contiennent les images des tweets
            for(int i = 0; i < list_tweetImagesPictureBox.Count; i = 0)
            {
                tweeterSplitContainer.Panel1.Controls.Remove(list_tweetImagesPictureBox[i]);
                list_tweetImagesPictureBox[i].Dispose();
                list_tweetImagesPictureBox.Remove(list_tweetImagesPictureBox[i]);
            }
            // Index pour la liste de PictureBox
            int intIndex = 0;
            int intCpt = 0; // Compteur du nombre d'exécution de la boucle foreach
            int intY = 30; // Position Y de départ des Label

            try // Essaie d'envoyer la requête, s'il n'y a pas d'erreur, continue.
            {
                // Envoi de la requête
                WebResponse getResponse = getRequest.GetResponse();
                // Récupération des données reçues et stockage dans un string
                StreamReader reader = new StreamReader(getResponse.GetResponseStream());
                string strJson = reader.ReadToEnd();
                getResponse.Close();

                // Création d'un tableau JSON avec le string des données reçues
                jArrayTweets = JArray.Parse(strJson);

                // Parcourt le tableau JSON et pour chaque tweet, 
                foreach (JObject joTweet in jArrayTweets.Children())
                {
                    // ajoute le nom d'utilisateur et le texte du tweet dans un label
                    tab_tweetsLabel[intCpt].Text = Convert.ToString(joTweet["user"]["name"]) + " a tweeté : \n\n" + Convert.ToString(joTweet["text"]);
                    // Définit la position du label
                    tab_tweetsLabel[intCpt].Location = new System.Drawing.Point(0, intY);
                    // Ajoute le label dans le conteneur voulu
                    tweeterSplitContainer.Panel1.Controls.Add(tab_tweetsLabel[intCpt]);
                    
                    try // Si le tweet possède des médias
                    {
                        JArray jaMedia = (JArray)joTweet["extended_entities"]["media"];
                        int intCptMedia = 0;
                        foreach (JObject joMedia in jaMedia)
                        {
                            // Si le media est bien une photo
                            if (Convert.ToString(jaMedia[intCptMedia]["type"]) == "photo")
                            {
                                // Ajoute une PictureBox avec l'image
                                list_tweetImagesPictureBox.Add(new PictureBox());
                                string strMediaUrl = Convert.ToString(jaMedia[intCptMedia]["media_url"]); // Donne l'url .jpg
                                intY += 135;
                                list_tweetImagesPictureBox[intIndex].Load(strMediaUrl);
                                list_tweetImagesPictureBox[intIndex].Name = "tweetImagePictureBox";
                                list_tweetImagesPictureBox[intIndex].Size = new System.Drawing.Size(225, 165);
                                list_tweetImagesPictureBox[intIndex].Location = new System.Drawing.Point(0, intY);
                                list_tweetImagesPictureBox[intIndex].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                tweeterSplitContainer.Panel1.Controls.Add(list_tweetImagesPictureBox[intIndex]);
                                intIndex++;
                                intY += 35;
                            }
                            intCptMedia++;
                        }
                    }
                    catch (NullReferenceException e)
                    {
                    }
                    // Auguemente la position Y et le compteur
                    intY += 135;
                    intCpt++;
                }
            }
            catch (WebException e)
            {
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée la requête http permettant d'obtenir les 20 derniers abonnements de l'utilisateur
        /// et l'envoie. Le résultat est obtenu en JSON
        /// </summary>
        private void findLastFollowings()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strUserId, "findFollowings");
            // Gestion des paramètres de la requête
            string postbody = "user_id=" + Uri.EscapeDataString(strUserId);

            WebRequest getRequest = createGetRequest(strAuthHeader, strGetFollowingsUrl + "?" + postbody);

            try
            {
                // Envoi de la requête
                WebResponse getResponse = getRequest.GetResponse();
                // Récupération des données reçues et stockage dans un string
                StreamReader reader = new StreamReader(getResponse.GetResponseStream());
                string strJson = reader.ReadToEnd();
                getResponse.Close();

                // Création d'un objet JSON avec le string des données reçues
                JObject joFollowings = JObject.Parse(strJson);

                emptyFollowingsFollowersLabels();

                int intCpt = 0; // Compteur du nombre d'exécution de la boucle foreach
                int intY = 5; // Position Y de départ des Label

                // Pour chaque objet utilisateur dans l'objet joFollowings
                foreach (JObject joUser in joFollowings["users"])
                {
                    // ajoute le nom d'utilisateur et le pseudo @XXX dans un label
                    tab_FollowingsFollowersLabel[intCpt].Text = Convert.ToString(joFollowings["users"][intCpt]["name"]) + " (@" + Convert.ToString(joFollowings["users"][intCpt]["screen_name"]) + ")";
                    // Définit la position du label
                    tab_FollowingsFollowersLabel[intCpt].Location = new System.Drawing.Point(0, intY);
                    // Ajoute le label dans le conteneur voulu
                    followingFollowersSplitContainer.Panel2.Controls.Add(tab_FollowingsFollowersLabel[intCpt]);
                    // Auguemente la position Y et le compteur
                    intY += 25;
                    intCpt++;
                }
            }
            catch (WebException e)
            {
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Crée la requête http permettant d'obtenir les 20 derniers abonnés de l'utilisateur
        /// et l'envoie. Le résultat est obtenu en JSON
        /// </summary>
        private void findLastFollowers()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strUserId, "findFollowers");
            // Gestion des paramètres de la requête
            string postbody = "user_id=" + Uri.EscapeDataString(strUserId);

            WebRequest getRequest = createGetRequest(strAuthHeader, strGetFollowersUrl + "?" + postbody);

            try
            {
                // Envoi de la requête
                WebResponse getResponse = getRequest.GetResponse();
                // Récupération des données reçues et stockage dans un string
                StreamReader reader = new StreamReader(getResponse.GetResponseStream());
                string strJson = reader.ReadToEnd();
                getResponse.Close();

                // Création d'un objet JSON avec le string des données reçues
                JObject joFollowers = JObject.Parse(strJson);

                emptyFollowingsFollowersLabels();

                int intCpt = 0; // Compteur du nombre d'exécution de la boucle foreach
                int intY = 5; // Position Y de départ des Label

                // Pour chaque objet utilisateur dans l'objet joFollowers
                foreach (JObject joUser in joFollowers["users"])
                {
                    // ajoute le nom d'utilisateur et le pseudo @XXX dans un label
                    tab_FollowingsFollowersLabel[intCpt].Text = Convert.ToString(joFollowers["users"][intCpt]["name"]) + " (@" + Convert.ToString(joFollowers["users"][intCpt]["screen_name"]) + ")";
                    // Définit la position du label
                    tab_FollowingsFollowersLabel[intCpt].Location = new System.Drawing.Point(0, intY);
                    // Ajoute le label dans le conteneur voulu
                    followingFollowersSplitContainer.Panel2.Controls.Add(tab_FollowingsFollowersLabel[intCpt]);
                    // Auguemente la position Y et le compteur
                    intY += 25;
                    intCpt++;
                }
            }
            catch (WebException e)
            {
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée la requête http permettant d'obtenir les 20 derniers tweets de la timeline de l'utilisateur
        /// et l'envoie. Le résultat est obtenu en JSON
        /// </summary>
        private void findTimeline()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strUserId, "findTimeline");

            WebRequest getRequest = createGetRequest(strAuthHeader, strGetTimelineUrl);

            // Vide la liste de PictureBox qui contiennent les images des tweets
            for (int i = 0; i < list_tweetImagesPictureBox.Count; i = 0)
            {
                timelineLikeSplitContainer.Panel2.Controls.Remove(list_tweetImagesPictureBox[i]);
                list_tweetImagesPictureBox[i].Dispose();
                list_tweetImagesPictureBox.Remove(list_tweetImagesPictureBox[i]);
            }
            // Index pour la liste de PictureBox
            int intIndex = 0;
            int intCpt = 0; // Compteur du nombre d'exécution de la boucle foreach
            int intY = 5; // Position Y de départ des Label

            try
            {
                // Envoi de la requête
                WebResponse getResponse = getRequest.GetResponse();
                // Récupération des données reçues et stockage dans un string
                StreamReader reader = new StreamReader(getResponse.GetResponseStream());
                string strJson = reader.ReadToEnd();
                getResponse.Close();

                // Création d'un tableau JSON avec le string des données reçues
                jaTimelineLikes = JArray.Parse(strJson);

                emptyTimelineLikesLabels();

                // Pour chaque tweet dans le tableau
                foreach (JObject joTweet in jaTimelineLikes.Children())
                {
                    // ajoute le nom d'utilisateur et le texte du tweet dans un label
                    tab_TimelineLikesLabel[intCpt].Text = Convert.ToString(joTweet["user"]["name"]) + " a tweeté : \n\n" + Convert.ToString(joTweet["text"]);
                    // Définit la position du label
                    tab_TimelineLikesLabel[intCpt].Location = new System.Drawing.Point(0, intY);
                    // Ajoute le label dans le conteneur voulu
                    timelineLikeSplitContainer.Panel2.Controls.Add(tab_TimelineLikesLabel[intCpt]);

                    try // Si le tweet possède des médias
                    {
                        JArray jaMedia = (JArray)joTweet["extended_entities"]["media"];
                        int intCptMedia = 0;
                        foreach (JObject joMedia in jaMedia)
                        {
                            // Si le media est bien une photo
                            if (Convert.ToString(jaMedia[intCptMedia]["type"]) == "photo")
                            {
                                // Ajoute une PictureBox avec l'image
                                list_tweetImagesPictureBox.Add(new PictureBox());
                                string strMediaUrl = Convert.ToString(jaMedia[intCptMedia]["media_url"]); // Donne l'url .jpg
                                intY += 115;
                                list_tweetImagesPictureBox[intIndex].Load(strMediaUrl);
                                list_tweetImagesPictureBox[intIndex].Name = "tweetImagePictureBox";
                                list_tweetImagesPictureBox[intIndex].Size = new System.Drawing.Size(340, 200);
                                list_tweetImagesPictureBox[intIndex].Location = new System.Drawing.Point(0, intY);
                                list_tweetImagesPictureBox[intIndex].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                timelineLikeSplitContainer.Panel2.Controls.Add(list_tweetImagesPictureBox[intIndex]);
                                intIndex++;
                                intY += 91;
                            }
                            intCptMedia++;
                        }
                    }
                    catch (NullReferenceException e)
                    {
                    }

                    // Auguemente la position Y et le compteur
                    intY += 115;
                    intCpt++;
                }
            }
            catch (WebException e)
            {
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée la requête http permettant d'obtenir les 20 derniers tweets aimés
        /// et l'envoie. Le résultat est obtenu en JSON
        /// </summary>
        private void findLikes()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strUserId, "findLikes");
            // Gestion des paramètres de la requête
            string postbody = "user_id=" + Uri.EscapeDataString(strUserId);

            WebRequest getRequest = createGetRequest(strAuthHeader, strGetLikesUrl + "?" + postbody);

            for (int i = 0; i < list_tweetImagesPictureBox.Count; i = 0)
            {
                timelineLikeSplitContainer.Panel2.Controls.Remove(list_tweetImagesPictureBox[i]);
                list_tweetImagesPictureBox[i].Dispose();
                list_tweetImagesPictureBox.Remove(list_tweetImagesPictureBox[i]);
            }

            int intIndex = 0;
            int intCpt = 0; // Compteur du nombre d'exécution de la boucle foreach
            int intY = 5; // Position Y de départ des Label

            try
            {
                // Envoi de la requête
                WebResponse getResponse = getRequest.GetResponse();
                // Récupération des données reçues et stockage dans un string
                StreamReader reader = new StreamReader(getResponse.GetResponseStream());
                string strJson = reader.ReadToEnd();
                getResponse.Close();

                // Création d'un tableau JSON avec le string des données reçues
                jaTimelineLikes = JArray.Parse(strJson);

                emptyTimelineLikesLabels();

                // Pour chaque tweet dans le tableau
                foreach (JObject joTweet in jaTimelineLikes.Children())
                {
                    // ajoute le nom d'utilisateur et le texte du tweet dans un label
                    tab_TimelineLikesLabel[intCpt].Text = Convert.ToString(joTweet["user"]["name"]) + " a tweeté : \n\n" + Convert.ToString(joTweet["text"]);
                    // Définit la position du label
                    tab_TimelineLikesLabel[intCpt].Location = new System.Drawing.Point(0, intY);
                    // Ajoute le label dans le conteneur voulu
                    timelineLikeSplitContainer.Panel2.Controls.Add(tab_TimelineLikesLabel[intCpt]);

                    try
                    {
                        JArray jaMedia = (JArray)joTweet["extended_entities"]["media"];
                        int intCptMedia = 0;
                        foreach (JObject joMedia in jaMedia)
                        {
                            // Si le media est bien une photo
                            if (Convert.ToString(jaMedia[intCptMedia]["type"]) == "photo")
                            {
                                // Ajoute une PictureBox avec l'image
                                list_tweetImagesPictureBox.Add(new PictureBox());
                                string strMediaUrl = Convert.ToString(jaMedia[intCptMedia]["media_url"]); // Donne l'url .jpg
                                intY += 115;
                                list_tweetImagesPictureBox[intIndex].Load(strMediaUrl);
                                list_tweetImagesPictureBox[intIndex].Name = "tweetImagePictureBox";
                                list_tweetImagesPictureBox[intIndex].Size = new System.Drawing.Size(340, 200);
                                list_tweetImagesPictureBox[intIndex].Location = new System.Drawing.Point(0, intY);
                                list_tweetImagesPictureBox[intIndex].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                timelineLikeSplitContainer.Panel2.Controls.Add(list_tweetImagesPictureBox[intIndex]);
                                intIndex++;
                                intY += 91;
                            }
                            intCptMedia++;
                        }
                    }
                    catch (NullReferenceException e)
                    {
                    }
                    // Auguemente la position Y et le compteur
                    intY += 115;
                    intCpt++;
                }
            }
            catch (WebException e)
            {
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Identifie le label cliqué et affiche les bons icones pour le like et retweet
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void label_Click(object sender, EventArgs e)
        {
            // Déclare un nouveau label
            Label clikedTextBox = new Label();
            // Le label devient le label cliqué (sender)
            clikedTextBox = sender as Label;

            // Numéro du label, position dans le tableau
            intLastClickedTweetIndex = 0;

            // Parcourt le tableau jusqu'à trouver le label correspondant au label cliqué
            for(int i = 0; i < 20; i++)
            {
                if(clikedTextBox.Handle == tab_TimelineLikesLabel[i].Handle)
                {
                    // Assigne le numéro du label et quitte la boucle
                    intLastClickedTweetIndex = i;
                    break;
                }
            }

            // Affiche les pictureBox
            likePictureBox.Visible = true;
            retweetPictureBox.Visible = true;

            // Si le tweet est aimé, affiche l'image liked (coeur plein)
            if (Convert.ToString(jaTimelineLikes[intLastClickedTweetIndex]["favorited"]) == "True")
            { 
                likePictureBox.BackgroundImage = TwitterClient.Properties.Resources.liked;
            }
            else // Sinon affiche l'image notLiked (coeur vide)
            {
                likePictureBox.BackgroundImage = TwitterClient.Properties.Resources.notLiked;
            }

            // Si le tweet est retweeté, affiche l'image retweeted (flèches pleines)
            if (Convert.ToString(jaTimelineLikes[intLastClickedTweetIndex]["retweeted"]) == "True")
            {
                retweetPictureBox.BackgroundImage = TwitterClient.Properties.Resources.retweeted;
            }
            else // Sinon affiche l'image notRetweeted (flèches vides)
            {
                retweetPictureBox.BackgroundImage = TwitterClient.Properties.Resources.notRetweeted;
            }
            strLastClickedTweetID = Convert.ToString(jaTimelineLikes[intLastClickedTweetIndex]["id"]);
        }

        /// <summary>
        /// Selon si le tweet est aimé ou pas, appelle la fonction correspondante
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void likePictureBox_Click(object sender, EventArgs e)
        {
            if(Convert.ToString(jaTimelineLikes[intLastClickedTweetIndex]["favorited"]) == "True")
            {
                unLikeTweet();
            }
            else
            {
                likeTweet();
            }
        }

        /// <summary>
        /// Crée et envoie la requête http permettant d'aimer un tweet
        /// </summary>
        private void likeTweet()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strLastClickedTweetID, "likeTweet");
            // Création du body de la requête
            string strPostBody = "id=" + Uri.EscapeDataString(strLastClickedTweetID);

            WebRequest postRequest = createPostRequest(strAuthHeader, strPostLikeTweetUrl, strPostBody);

            try // Permet la gestion des exceptions si le serveur retourne une erreur.
            {
                // Envoi de la requête
                WebResponse postResponse = postRequest.GetResponse();
                postResponse.Close();

                // Avertit l'utilisateur
                MessageBox.Show(string.Format(STR_LIKE_BOX_MESSAGE, strLastClickedTweetID), STR_LIKE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                likePictureBox.Visible = false;
                retweetPictureBox.Visible = false;

                // Raffraichit la liste de tweets
                if (timelineToolStripMenuItem.BackColor == Color.LightGray)
                {
                    findTimeline();
                }
                else
                {
                    findLikes();
                }
            }
            catch (WebException e) // Si une erreur se produit
            {
                // Récupère le message d'erreur et l'affiche dans une MessageBox
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée et envoie la requête http permettant de ne plus aimer un tweet
        /// </summary>
        private void unLikeTweet()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strLastClickedTweetID, "unlikeTweet");
            // Création du body de la requête
            string strPostBody = "id=" + Uri.EscapeDataString(strLastClickedTweetID);

            WebRequest postRequest = createPostRequest(strAuthHeader, strPostLikeTweetUrl, strPostBody);

            try // Permet la gestion des exceptions si le serveur retourne une erreur.
            {
                // Envoi de la requête
                WebResponse postResponse = postRequest.GetResponse();
                postResponse.Close();

                // Avertit l'utilisateur
                MessageBox.Show(string.Format(STR_UNLIKE_BOX_MESSAGE, strLastClickedTweetID), STR_LIKE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                likePictureBox.Visible = false;
                retweetPictureBox.Visible = false;

                // Raffraichit la liste de tweets
                if (timelineToolStripMenuItem.BackColor == Color.LightGray)
                {
                    findTimeline();
                }
                else
                {
                    findLikes();
                }
            }
            catch (WebException e) // Si une erreur se produit
            {
                // Récupère le message d'erreur et l'affiche dans une MessageBox
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Selon si le tweet est retweeté ou pas, appelle la fonction correspondante
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void retweetPictureBox_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(jaTimelineLikes[intLastClickedTweetIndex]["retweeted"]) == "True")
            {
                unretweet();
            }
            else
            {
                retweet();
            }
        }

        /// <summary>
        /// Crée et envoie la requête http permettant de retweeter
        /// </summary>
        private void retweet()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strLastClickedTweetID, "retweet");

            WebRequest postRequest = createPostRequest(strAuthHeader, string.Format(strPostRetweetUrl, strLastClickedTweetID), "");

            try // Permet la gestion des exceptions si le serveur retourne une erreur.
            {
                // Envoi de la requête
                WebResponse postResponse = postRequest.GetResponse();
                postResponse.Close();

                // Avertit l'utilisateur du retweet
                MessageBox.Show(string.Format(STR_RETWEET_BOX_MESSAGE, strLastClickedTweetID), STR_RETWEET_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                likePictureBox.Visible = false;
                retweetPictureBox.Visible = false;

                // Raffraichit la liste de tweets
                if (timelineToolStripMenuItem.BackColor == Color.LightGray)
                {
                    findTimeline();
                }
                else
                {
                    findLikes();
                }
            }
            catch (WebException e) // Si une erreur se produit
            {
                // Récupère le message d'erreur et l'affiche dans une MessageBox
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crée et envoie la requête http permettant d'enlever le retweet
        /// </summary>
        private void unretweet()
        {
            // Génération du header avec les paramètres d'authentification
            string strAuthHeader = generateAuthorizationHeader(strLastClickedTweetID, "unretweet");

            WebRequest postRequest = createPostRequest(strAuthHeader, string.Format(strPostUnretweetUrl, strLastClickedTweetID), "");

            try // Permet la gestion des exceptions si le serveur retourne une erreur.
            {
                // Envoi de la requête
                WebResponse postResponse = postRequest.GetResponse();
                postResponse.Close();

                // Avertit l'utilisateur
                MessageBox.Show(string.Format(STR_UNRETWEET_BOX_MESSAGE, strLastClickedTweetID), STR_RETWEET_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);

                likePictureBox.Visible = false;
                retweetPictureBox.Visible = false;

                // Raffraichit la liste de tweets
                if (timelineToolStripMenuItem.BackColor == Color.LightGray)
                {
                    findTimeline();
                }
                else
                {
                    findLikes();
                }
            }
            catch (WebException e) // Si une erreur se produit
            {
                // Récupère le message d'erreur et l'affiche dans une MessageBox
                string strErreur = Convert.ToString(e.Message);
                MessageBox.Show(strErreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vide le texte des labels contenant les abonnements ou abonnés
        /// </summary>
        private void emptyFollowingsFollowersLabels()
        {
            // Parcourt tout le tableau
            for(int i = 0; i < 20; i++)
            {
                // Si le label est déjà vide, sort de la boucle
                if(tab_FollowingsFollowersLabel[i].Text == "")
                {
                    break;
                }
                // Enlève le label du panel et supprime le texte présent
                tab_FollowingsFollowersLabel[i].Text = "";
                followingFollowersSplitContainer.Panel2.Controls.Remove(tab_FollowingsFollowersLabel[i]);

            }
        }

        /// <summary>
        /// Vide le texte des labels contenant la timeline et les tweets aimés
        /// </summary>
        private void emptyTimelineLikesLabels()
        {
            // Parcourt tout le tableau
            for (int i = 0; i < 20; i++)
            {
                // Si le label est déjà vide, sort de la boucle
                if (tab_TimelineLikesLabel[i].Text == "")
                {
                    break;
                }
                // Enlève le label du panel et supprime le texte présent
                tab_TimelineLikesLabel[i].Text = "";
                timelineLikeSplitContainer.Panel2.Controls.Remove(tab_TimelineLikesLabel[i]);

            }
        }

        /// <summary>
        /// Traite le texte à tweeter avant de l'envoyer à la fonction créant la requête http
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void tweeterButton_Click(object sender, EventArgs e)
        {
            // Tweet à poster
            string strStatus = tweeterRichTextBox.Text;

            // N'y a-t-il que des espaces ?
            bool boolOnlySpaces = true;

            // Vérifie que le texte n'est pas constitué uniquement d'espace
            for (int i = 0; i < strStatus.Length; i++)
            {
                if (strStatus[i] != Convert.ToChar(" "))
                {
                    boolOnlySpaces = false;
                }
            }

            // Si le tweet n'est pas vide et ne dépasse pas les 140 caractères propre à Twitter, il est envoyé.
            if (strStatus.Length <= 140 && strStatus.Length > 0 && !boolOnlySpaces)
            {
                sendTweet(strStatus);
                tweeterRichTextBox.Text = "";
                // Provoque la réactualisation des derniers tweets
                findLastTweets();
            }
            else
            {
                MessageBox.Show("Votre tweet doit faire entre 1 et 140 caractères !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Affiche l'interface qui permet de tweeter
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void tweeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cacher toutes les interfaces
            hideAllInterfaces();
            // Afficher l'interface pour tweeter
            tweeterSplitContainer.Visible = true;
            // Bouton permettant de sauvegarder les tweets
            saveTweetsButton.Visible = true;

            // Création d'un objet ToolStripMenuItem
            ToolStripMenuItem selectedMenu = new ToolStripMenuItem();
            // Récupération du sender
            selectedMenu = sender as ToolStripMenuItem;
            // et envoi de l'objet en paramètre de la fonction
            changeMenuColor(selectedMenu);
            // Recharger les 20 derniers tweets
            findLastTweets();
        }

        /// <summary>
        /// Affiche l'interface qui permet de voir ses abonnments
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void followingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cacher toutes les interfaces
            hideAllInterfaces();
            // Afficher l'interface pour voir les abonnements
            followingFollowersSplitContainer.Visible = true;
            followingLabel.Visible = true;

            // Création d'un objet ToolStripMenuItem
            ToolStripMenuItem selectedMenu = new ToolStripMenuItem();
            // Récupération du sender
            selectedMenu = sender as ToolStripMenuItem;
            // et envoi de l'objet en paramètre de la fonction
            changeMenuColor(selectedMenu);

            // Affiche les 20 derniers abonnements
            findLastFollowings();
        }

        /// <summary>
        /// Affiche l'interface qui permet de voir ses abonnés
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void followersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cacher toutes les interfaces
            hideAllInterfaces();
            // Afficher l'interface pour voir les abonnés
            followingFollowersSplitContainer.Visible = true;
            followersLabel.Visible = true;

            // Création d'un objet ToolStripMenuItem
            ToolStripMenuItem selectedMenu = new ToolStripMenuItem();
            // Récupération du sender
            selectedMenu = sender as ToolStripMenuItem;
            // et envoi de l'objet en paramètre de la fonction
            changeMenuColor(selectedMenu);

            // Affiche les 20 derniers abonnés
            findLastFollowers();
        }

        /// <summary>
        /// Affiche l'interface qui permet de voir sa timeline
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void timelineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cacher toutes les interfaces
            hideAllInterfaces();
            // Afficher l'interface pour voir la timeline
            timelineLikeSplitContainer.Visible = true;
            timelineLabel.Visible = true;
            // Bouton permettant de sauvegarder les tweets
            saveTweetsButton.Visible = true;

            // Création d'un objet ToolStripMenuItem
            ToolStripMenuItem selectedMenu = new ToolStripMenuItem();
            // Récupération du sender
            selectedMenu = sender as ToolStripMenuItem;
            // et envoi de l'objet en paramètre de la fonction
            changeMenuColor(selectedMenu);

            // Affiche la timeline
            findTimeline();
        }

        /// <summary>
        /// Affiche l'interface qui permet de voir les tweets aimés
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void likeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cacher toutes les interfaces
            hideAllInterfaces();
            // Afficher l'interface pour voir les tweets aimés
            timelineLikeSplitContainer.Visible = true;
            likeLabel.Visible = true;
            // Bouton permettant de sauvegarder les tweets
            saveTweetsButton.Visible = true;

            // Création d'un objet ToolStripMenuItem
            ToolStripMenuItem selectedMenu = new ToolStripMenuItem();
            // Récupération du sender
            selectedMenu = sender as ToolStripMenuItem;
            // et envoi de l'objet en paramètre de la fonction
            changeMenuColor(selectedMenu);

            // Affiche les tweets aimés
            findLikes();
        }

        /// <summary>
        /// Change la couleur de fond de l'élément du menu sélectionné
        /// et s'assure que le reste du menu est de couleur neutre.
        /// </summary>
        /// <param name="selectedMenu">Elément du menu à mettre en couleur</param>
        private void changeMenuColor(ToolStripMenuItem selectedMenu)
        {
            // Remet la couleur par défaut à tous les éléments du menu
            tweeterToolStripMenuItem.BackColor = DefaultBackColor;
            followingToolStripMenuItem.BackColor = DefaultBackColor;
            followersToolStripMenuItem.BackColor = DefaultBackColor;
            timelineToolStripMenuItem.BackColor = DefaultBackColor;
            likeToolStripMenuItem.BackColor = DefaultBackColor;

            // Accentue le menu sélectionné
            selectedMenu.BackColor = Color.LightGray;
        }

        /// <summary>
        /// Cache les différentes interfaces graphiques
        /// </summary>
        private void hideAllInterfaces()
        {
            tweeterSplitContainer.Visible = false;
            followingFollowersSplitContainer.Visible = false;
            timelineLikeSplitContainer.Visible = false;

            followingLabel.Visible = false;
            followersLabel.Visible = false;
            likeLabel.Visible = false;
            timelineLabel.Visible = false;
            likePictureBox.Visible = false;
            retweetPictureBox.Visible = false;
            saveTweetsButton.Visible = false;
        }

        /// <summary>
        /// Sauvegarde les tweets dans un fichier texte
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement
        /// </param>
        private void saveTweetsButton_Click(object sender, EventArgs e)
        {
            // Si l'on se trouve sur l'interface pour tweeter
            if(tweeterToolStripMenuItem.BackColor == Color.LightGray)
            {   
                // Ajoute un titre au début du tableau
                tab_strSavedTweets[0] = "Vos derniers tweets : ";
                int intIndex = 1;
                for(int i = 0; i < tab_tweetsLabel.Length; i++)
                {
                    // Récupère le contenu des labels
                    tab_strSavedTweets[intIndex] = tab_tweetsLabel[i].Text;
                    intIndex++;
                }
                // Crée est écrit le fichier
                System.IO.File.WriteAllLines(@"C:\Users\savaryal\Desktop\LastTweets.txt", tab_strSavedTweets);
                
            }
            // Si l'on se trouve sur l'interface de la timeline
            else if (timelineToolStripMenuItem.BackColor == Color.LightGray)
            {
                // AJoute un titre au début du tableau
                tab_strSavedTweets[0] = "Les derniers tweets de votre timeline : ";
                int intIndex = 1;
                for (int i = 0; i < tab_TimelineLikesLabel.Length; i++)
                {   
                    // Récupère le contenu des labels
                    tab_strSavedTweets[intIndex] = tab_TimelineLikesLabel[i].Text;
                    intIndex++;
                }
                // Crée et écrit le fichier
                System.IO.File.WriteAllLines(@"C:\Users\savaryal\Desktop\LastTimelineTweets.txt", tab_strSavedTweets);
            }
            else
            {
                // Ajoute un titre au début du tableau
                tab_strSavedTweets[0] = "Les derniers tweets aimés : ";
                int intIndex = 1;
                for (int i = 0; i < tab_TimelineLikesLabel.Length; i++)
                {
                    // Récupère le contenu des labels
                    tab_strSavedTweets[intIndex] = tab_TimelineLikesLabel[i].Text;
                    intIndex++;
                }
                // Crée et écrit le fichier
                System.IO.File.WriteAllLines(@"C:\Users\savaryal\Desktop\LastLikedTweets.txt", tab_strSavedTweets);
            }

            // Avertit l'utilisateur
            MessageBox.Show(STR_SAVE_BOX_MESSAGE, STR_SAVE_BOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Demande une confirmation à la fermeture du programme
        /// </summary>
        /// <param name="sender">Contrôle provoquant l'événement</param>
        /// <param name="e">Données liées à l'événement</param>
        private void twitterClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Si l'on clique sur le bouton "Non" 
            if (DialogResult.No == MessageBox.Show(STR_QUIT_BOX_MESSAGE, STR_QUIT_BOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                // Annule la fermeture
                e.Cancel = true;
            }
        }
    }
}
